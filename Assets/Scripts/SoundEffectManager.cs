using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// ��������Ѻ�Ѵ��� Sound Effects ���������
public class SoundEffectManager : MonoBehaviour
{
    // Singleton instance �ͧ SoundEffectManager
    public static SoundEffectManager Instance { get; private set; }

    // ����๹�� AudioSource ������㹡����� Sound Effects �����
    private static AudioSource audioSource;

    // ����๹�� AudioSource ����Ѻ���§����ա������ Pitch
    private static AudioSource ramdomPicthAudioSource;
    // ����๹�� AudioSource ����Ѻ���§�ҡ���������§�ٴ����Ф�
    private static AudioSource voiceAudioSource;

    // ��ҧ�ԧ�֧ SoundEffectLibrary ���ʹ֧��Ի���§
    private static SoundEffectLibrary soundEffectLibrary;

    // ��ҧ�ԧ�֧ UI Slider ����Ѻ�Ǻ����дѺ���§ SFX
    [SerializeField]
    private Slider sfxSlider;

    /// ������Ѻ��駤�� Singleton ��д֧����๹�������
    private void Awake()
    {
        // ��Ǩ�ͺ����� Instance �ͧ SoundEffectManager ���������������
        if (Instance == null)
        {
            // ����ѧ����� ����˹� Instance �繵�ǹ��
            Instance = this;

            // �֧����๹�� AudioSource ��ѡ
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("SoundEffectManager: No AudioSource component found on this GameObject for general SFX.", this);
                // ��� return ��������Ǩ�ͺ AudioSource ���� ������ (�����)
            }

            // ��Ǩ�ͺ�������� AudioSource ����Ѻ randomPitch
            if (ramdomPicthAudioSource == null)
            {
                ramdomPicthAudioSource = gameObject.AddComponent<AudioSource>();
                ramdomPicthAudioSource.playOnAwake = false;
                ramdomPicthAudioSource.loop = false; // �·���� SFX �����ǹ���
                // ��駤������ �� outputAudioMixerGroup, spatialBlend
            }

            // ��Ǩ�ͺ�������� AudioSource ����Ѻ voice
            if (voiceAudioSource == null)
            {
                voiceAudioSource = gameObject.AddComponent<AudioSource>();
                voiceAudioSource.playOnAwake = false;
                voiceAudioSource.loop = false; // �·���� Voice �����ǹ���
                // ��駤������ �� outputAudioMixerGroup, spatialBlend
            }
            // *** ����ش��ǹ����������� Initialize ***

            // �֧����๹�� SoundEffectLibrary ���Ṻ�ҡѺ GameObject ���ǡѹ
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            if (soundEffectLibrary == null)
            {
                Debug.LogError("SoundEffectManager: No SoundEffectLibrary component found on this GameObject.", this);
                return; // �������� Library ���������ö������§��
            }

            // ��ͧ�ѹ������ GameObject ���١������������Ŵ Scene ����
            // DontDestroyOnLoad(gameObject); // �س����������÷Ѵ��������
        }
        else
        {
            // ����� Instance �������� ������� GameObject ������ͻ�ͧ�ѹ Manager ��ӫ�͹
            Debug.LogWarning("SoundEffectManager: Attempted to create a second instance. Destroying new instance.", this);
            Destroy(gameObject);
        }
    }

    /// ������Ѻ���� Listener ���Ѻ Slider
    private void Start()
    {
        if (sfxSlider != null)
        {
            // ���� Listener ���Ѻ�˵ء�ó� onValueChanged �ͧ Slider ����ͤ�Ңͧ Slider ����¹�ŧ �����¡���ʹ OnValueChanged()
            sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });

            // ��駤��������鹢ͧ Volume ���ç�Ѻ��һѨ�غѹ�ͧ Slider
            SetVolume(sfxSlider.value);
        }
        else
        {
            Debug.LogWarning("SoundEffectManager: SFX Slider is not assigned in the Inspector.", this);
        }
    }

    /// ��蹤�Ի���§Ẻ�����ҡ��������§����к� (���� AudioSource ��ѡ)
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

    /// ��蹤�Ի���§��������� Pitch
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
            ramdomPicthAudioSource.pitch = Random.Range(minPitch, maxPitch); // ������� Pitch
            ramdomPicthAudioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogWarning($"SoundEffectManager: No audio clip found for sound group: {soundName}");
        }
    }

    /// ������§�ҡ���������§�ٴ (���� AudioSource ੾������Ѻ Voice)
    public static void PlayVoice(string soundName)
    {
        if (Instance == null || voiceAudioSource == null || soundEffectLibrary == null)
        {
            Debug.LogWarning("SoundEffectManager: Cannot play voice sound. Manager not initialized or missing components (voiceAudioSource).", Instance);
            return;
        }

        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName); // �����Ҩ���� GetSpecificClip ����Ѻ voice

        if (audioClip != null)
        {
            voiceAudioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogWarning($"SoundEffectManager: No audio clip found for voice sound group: {soundName}");
        }
    }

    /// ��駤���дѺ���§�ͧ Sound Effects (��Ѻ AudioSource ��ѡ)
    public static void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
        // �س�Ҩ��ͧ��äǺ��� volume �ͧ ramdomPicthAudioSource ��� voiceAudioSource �¡�ѹ
        if (ramdomPicthAudioSource != null)
        {
            ramdomPicthAudioSource.volume = volume; // ���͡�˹��¡
        }
        if (voiceAudioSource != null)
        {
            voiceAudioSource.volume = volume; // ���͡�˹��¡
        }
        else
        {
            Debug.LogWarning("SoundEffectManager: Cannot set volume. AudioSource is null.", Instance);
        }
    }

    /// ���ʹ���١���¡����ͤ�Ңͧ sfxSlider �ա������¹�ŧ
    public void OnValueChanged()
    {
        if (sfxSlider != null)
        {
            SetVolume(sfxSlider.value);
        }
    }
}