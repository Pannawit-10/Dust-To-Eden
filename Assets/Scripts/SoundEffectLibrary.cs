using System; // ����Ѻ System.Serializable
using System.Collections.Generic; // ����Ѻ Dictionary ��� List
using UnityEngine; // ����Ѻ MonoBehaviour, AudioClip, Random

// �ź��������Ѻ�Ѵ����ШѴ��á������Ի���§
public class SoundEffectLibrary : MonoBehaviour
{
    // ��Ŵ�������ö Serialized �� �����������ö��˹������� Unity Editor
    [SerializeField]
    private SoundEffectGroup[] soundEffectGroups; // ��������ͧ��������§

    // Dictionary ����Ѻ�Ѵ�纡�������§ ���ժ��͡������ Key �����¡�� AudioClip �� Value
    private Dictionary<string, List<AudioClip>> soundDictionary;

    // ������Ѻ������� Dictionary
    private void Awake()
    {
        InitializeDictionary();
    }

    // ����������������������Ѻ soundDictionary �ҡ soundEffectGroups
    private void InitializeDictionary()
    {
        // ���ҧ Dictionary ����
        soundDictionary = new Dictionary<string, List<AudioClip>>();

        // ǹ�ٻ��ҹ���� SoundEffectGroup � soundEffectGroups
        foreach (SoundEffectGroup soundEffectGroup in soundEffectGroups)
        {
            // ������������§ŧ� Dictionary ������͡������ Key �����¡�� AudioClip �ͧ���������� Value
            soundDictionary[soundEffectGroup.name] = soundEffectGroup.audioClips;
        }
    }

    public AudioClip GetRandomClip(string name)
    {
        // ��Ǩ�ͺ��� soundDictionary �� Key (���͡����) ����к��������
        if (soundDictionary.ContainsKey(name))
        {
            // ����� ���֧��¡�� AudioClip �ͧ��������
            List<AudioClip> audioClips = soundDictionary[name];

            // ��Ǩ�ͺ����դ�Ի���§���¡���������
            if (audioClips != null && audioClips.Count > 0)
            {
                // �觤׹��Ի���§Ẻ�����ҡ��¡��
                // UnityEngine.Random.Range(0, audioClips.Count) �����ҧ�Ţ���������ҧ 0 (���) �֧ Count (������)
                return audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
            }
        }

        // �ҡ��辺��������͡������ҧ���� ����觤׹ null
        return null;
    }
}

// Struct ����Ѻ�Ѵ�������Ի���§���ª���
[System.Serializable]
public struct SoundEffectGroup
{
    public string name; // ���ͧ͢��������§
    public List<AudioClip> audioClips; // ��¡�âͧ��Ի���§㹡�������
}