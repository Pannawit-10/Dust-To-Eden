using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// คลาสสำหรับจัดการ Sound Effects ทั้งหมดในเกม
public class SoundEffectManager : MonoBehaviour
{
    // Singleton instance ของ SoundEffectManager
    public static SoundEffectManager Instance { get; private set; }

    // คอมโพเนนต์ AudioSource ที่จะใช้ในการเล่น Sound Effects ทั่วไป
    private static AudioSource audioSource;

    // คอมโพเนนต์ AudioSource สำหรับเสียงที่มีการสุ่ม Pitch
    private static AudioSource ramdomPicthAudioSource;
    // คอมโพเนนต์ AudioSource สำหรับเสียงพากย์หรือเสียงพูดตัวละคร
    private static AudioSource voiceAudioSource;

    // อ้างอิงถึง SoundEffectLibrary เพื่อดึงคลิปเสียง
    private static SoundEffectLibrary soundEffectLibrary;

    // อ้างอิงถึง UI Slider สำหรับควบคุมระดับเสียง SFX
    [SerializeField]
    private Slider sfxSlider;

    /// ใช้สำหรับตั้งค่า Singleton และดึงคอมโพเนนต์ที่จำเป็น
    private void Awake()
    {
        // ตรวจสอบว่ามี Instance ของ SoundEffectManager อยู่แล้วหรือไม่
        if (Instance == null)
        {
            // ถ้ายังไม่มี ให้กำหนด Instance เป็นตัวนี้
            Instance = this;

            // ดึงคอมโพเนนต์ AudioSource หลัก
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("SoundEffectManager: No AudioSource component found on this GameObject for general SFX.", this);
                // ไม่ return เพื่อให้ตรวจสอบ AudioSource อื่นๆ ต่อไปได้ (ถ้ามี)
            }

            // ตรวจสอบหรือเพิ่ม AudioSource สำหรับ randomPitch
            if (ramdomPicthAudioSource == null)
            {
                ramdomPicthAudioSource = gameObject.AddComponent<AudioSource>();
                ramdomPicthAudioSource.playOnAwake = false;
                ramdomPicthAudioSource.loop = false; // โดยทั่วไป SFX ไม่ได้วนซ้ำ
                // ตั้งค่าอื่นๆ เช่น outputAudioMixerGroup, spatialBlend
            }

            // ตรวจสอบหรือเพิ่ม AudioSource สำหรับ voice
            if (voiceAudioSource == null)
            {
                voiceAudioSource = gameObject.AddComponent<AudioSource>();
                voiceAudioSource.playOnAwake = false;
                voiceAudioSource.loop = false; // โดยทั่วไป Voice ไม่ได้วนซ้ำ
                // ตั้งค่าอื่นๆ เช่น outputAudioMixerGroup, spatialBlend
            }
            // *** สิ้นสุดส่วนที่เพิ่มเพื่อ Initialize ***

            // ดึงคอมโพเนนต์ SoundEffectLibrary ที่แนบมากับ GameObject เดียวกัน
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            if (soundEffectLibrary == null)
            {
                Debug.LogError("SoundEffectManager: No SoundEffectLibrary component found on this GameObject.", this);
                return; // ถ้าไม่มี Library ก็ไม่สามารถเล่นเสียงได้
            }

            // ป้องกันไม่ให้ GameObject นี้ถูกทำลายเมื่อโหลด Scene ใหม่
            // DontDestroyOnLoad(gameObject); // คุณได้คอมเมนต์บรรทัดนี้ไปแล้ว
        }
        else
        {
            // ถ้ามี Instance อยู่แล้ว ให้ทำลาย GameObject นี้เพื่อป้องกัน Manager ซ้ำซ้อน
            Debug.LogWarning("SoundEffectManager: Attempted to create a second instance. Destroying new instance.", this);
            Destroy(gameObject);
        }
    }

    /// ใช้สำหรับเพิ่ม Listener ให้กับ Slider
    private void Start()
    {
        if (sfxSlider != null)
        {
            // เพิ่ม Listener ให้กับเหตุการณ์ onValueChanged ของ Slider เมื่อค่าของ Slider เปลี่ยนแปลง จะเรียกเมธอด OnValueChanged()
            sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });

            // ตั้งค่าเริ่มต้นของ Volume ให้ตรงกับค่าปัจจุบันของ Slider
            SetVolume(sfxSlider.value);
        }
        else
        {
            Debug.LogWarning("SoundEffectManager: SFX Slider is not assigned in the Inspector.", this);
        }
    }

    /// เล่นคลิปเสียงแบบสุ่มจากกลุ่มเสียงที่ระบุ (โดยใช้ AudioSource หลัก)
    public static void Play(string soundName)
    {
        if (Instance == null || audioSource == null || soundEffectLibrary == null)
        {
            Debug.LogWarning("SoundEffectManager: Cannot play sound. Manager not initialized or missing components.", Instance);
            return;
        }

        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);

        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogWarning($"SoundEffectManager: No audio clip found for sound group: {soundName}");
        }
    }

    /// เล่นคลิปเสียงพร้อมสุ่ม Pitch
    public static void PlayRandomPitch(string soundName, float minPitch = 0.9f, float maxPitch = 1.1f)
    {
        if (Instance == null || ramdomPicthAudioSource == null || soundEffectLibrary == null)
        {
            Debug.LogWarning("SoundEffectManager: Cannot play random pitch sound. Manager not initialized or missing components (ramdomPicthAudioSource).", Instance);
            return;
        }

        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);

        if (audioClip != null)
        {
            ramdomPicthAudioSource.pitch = Random.Range(minPitch, maxPitch); // สุ่มค่า Pitch
            ramdomPicthAudioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogWarning($"SoundEffectManager: No audio clip found for sound group: {soundName}");
        }
    }

    /// เล่นเสียงพากย์หรือเสียงพูด (โดยใช้ AudioSource เฉพาะสำหรับ Voice)
    public static void PlayVoice(string soundName)
    {
        if (Instance == null || voiceAudioSource == null || soundEffectLibrary == null)
        {
            Debug.LogWarning("SoundEffectManager: Cannot play voice sound. Manager not initialized or missing components (voiceAudioSource).", Instance);
            return;
        }

        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName); // หรืออาจจะมี GetSpecificClip สำหรับ voice

        if (audioClip != null)
        {
            voiceAudioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogWarning($"SoundEffectManager: No audio clip found for voice sound group: {soundName}");
        }
    }

    /// ตั้งค่าระดับเสียงของ Sound Effects (ใช้กับ AudioSource หลัก)
    public static void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
        // คุณอาจต้องการควบคุม volume ของ ramdomPicthAudioSource และ voiceAudioSource แยกกัน
        if (ramdomPicthAudioSource != null)
        {
            ramdomPicthAudioSource.volume = volume; // หรือกำหนดแยก
        }
        if (voiceAudioSource != null)
        {
            voiceAudioSource.volume = volume; // หรือกำหนดแยก
        }
        else
        {
            Debug.LogWarning("SoundEffectManager: Cannot set volume. AudioSource is null.", Instance);
        }
    }

    /// เมธอดที่ถูกเรียกเมื่อค่าของ sfxSlider มีการเปลี่ยนแปลง
    public void OnValueChanged()
    {
        if (sfxSlider != null)
        {
            SetVolume(sfxSlider.value);
        }
    }
}