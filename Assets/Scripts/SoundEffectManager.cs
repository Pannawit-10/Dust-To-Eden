using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// ��������Ѻ�Ѵ��� Sound Effects ���������
public class SoundEffectManager : MonoBehaviour
{
    // Singleton instance �ͧ SoundEffectManager
    public static SoundEffectManager Instance { get; private set; }

    // ����๹�� AudioSource ������㹡����� Sound Effects
    private static AudioSource audioSource;

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

            // �֧����๹�� AudioSource ���Ṻ�ҡѺ GameObject ���ǡѹ
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("SoundEffectManager: No AudioSource component found on this GameObject.", this);
                return;
            }

            // �֧����๹�� SoundEffectLibrary ���Ṻ�ҡѺ GameObject ���ǡѹ
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            if (soundEffectLibrary == null)
            {
                Debug.LogError("SoundEffectManager: No SoundEffectLibrary component found on this GameObject.", this);
                return;
            }

            // ��ͧ�ѹ������ GameObject ���١������������Ŵ Scene ����
            DontDestroyOnLoad(gameObject);
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

    /// ��蹤�Ի���§Ẻ�����ҡ��������§����к�
    public static void Play(string soundName)
    {
        // ��Ǩ�ͺ��� Instance ��� AudioSource �������ҹ�������
        if (Instance == null || audioSource == null || soundEffectLibrary == null)
        {
            Debug.LogWarning("SoundEffectManager: Cannot play sound. Manager not initialized or missing components.", Instance);
            return;
        }

        // �֧ AudioClip Ẻ�����ҡ SoundEffectLibrary
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);

        // ����� AudioClip �� ���������§���
        if (audioClip != null)
        {
            // PlayOneShot ������Ѻ������§������ú�ǹ���§�����ѧ�������
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogWarning($"SoundEffectManager: No audio clip found for sound group: {soundName}");
        }
    }

    /// ��駤���дѺ���§�ͧ Sound Effects
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

    /// ���ʹ���١���¡����ͤ�Ңͧ sfxSlider �ա������¹�ŧ
    public void OnValueChanged()
    {
        if (sfxSlider != null)
        {
            // ��駤���дѺ���§�����һѨ�غѹ�ͧ Slider
            SetVolume(sfxSlider.value);
        }
    }
}