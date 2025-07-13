using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// คลาสสำหรับจัดการ Sound Effects ทั้งหมดในเกม
public class SoundEffectManager : MonoBehaviour
{
    // Singleton instance ของ SoundEffectManager
    public static SoundEffectManager Instance { get; private set; }

    // คอมโพเนนต์ AudioSource ที่จะใช้ในการเล่น Sound Effects
    private static AudioSource audioSource;

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

            // ดึงคอมโพเนนต์ AudioSource ที่แนบมากับ GameObject เดียวกัน
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("SoundEffectManager: No AudioSource component found on this GameObject.", this);
                return;
            }

            // ดึงคอมโพเนนต์ SoundEffectLibrary ที่แนบมากับ GameObject เดียวกัน
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            if (soundEffectLibrary == null)
            {
                Debug.LogError("SoundEffectManager: No SoundEffectLibrary component found on this GameObject.", this);
                return;
            }

            // ป้องกันไม่ให้ GameObject นี้ถูกทำลายเมื่อโหลด Scene ใหม่
            DontDestroyOnLoad(gameObject);
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

    /// เล่นคลิปเสียงแบบสุ่มจากกลุ่มเสียงที่ระบุ
    public static void Play(string soundName)
    {
        // ตรวจสอบว่า Instance และ AudioSource พร้อมใช้งานหรือไม่
        if (Instance == null || audioSource == null || soundEffectLibrary == null)
        {
            Debug.LogWarning("SoundEffectManager: Cannot play sound. Manager not initialized or missing components.", Instance);
            return;
        }

        // ดึง AudioClip แบบสุ่มจาก SoundEffectLibrary
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);

        // ถ้าได้ AudioClip มา ให้เล่นเสียงนั้น
        if (audioClip != null)
        {
            // PlayOneShot ใช้สำหรับเล่นเสียงที่ไม่รบกวนเสียงที่กำลังเล่นอยู่
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogWarning($"SoundEffectManager: No audio clip found for sound group: {soundName}");
        }
    }

    /// ตั้งค่าระดับเสียงของ Sound Effects
    public static void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
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
            // ตั้งค่าระดับเสียงตามค่าปัจจุบันของ Slider
            SetVolume(sfxSlider.value);
        }
    }
}