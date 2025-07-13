using System; // สำหรับ System.Serializable
using System.Collections.Generic; // สำหรับ Dictionary และ List
using UnityEngine; // สำหรับ MonoBehaviour, AudioClip, Random

// ไลบรารีสำหรับจัดเก็บและจัดการกลุ่มคลิปเสียง
public class SoundEffectLibrary : MonoBehaviour
{
    // ฟิลด์ที่สามารถ Serialized ได้ เพื่อให้สามารถกำหนดค่าได้ใน Unity Editor
    [SerializeField]
    private SoundEffectGroup[] soundEffectGroups; // อาร์เรย์ของกลุ่มเสียง

    // Dictionary สำหรับจัดเก็บกลุ่มเสียง โดยมีชื่อกลุ่มเป็น Key และรายการ AudioClip เป็น Value
    private Dictionary<string, List<AudioClip>> soundDictionary;

    // ใช้สำหรับเริ่มต้น Dictionary
    private void Awake()
    {
        InitializeDictionary();
    }

    // เริ่มต้นและเติมข้อมูลให้กับ soundDictionary จาก soundEffectGroups
    private void InitializeDictionary()
    {
        // สร้าง Dictionary ใหม่
        soundDictionary = new Dictionary<string, List<AudioClip>>();

        // วนลูปผ่านแต่ละ SoundEffectGroup ใน soundEffectGroups
        foreach (SoundEffectGroup soundEffectGroup in soundEffectGroups)
        {
            // เพิ่มกลุ่มเสียงลงใน Dictionary โดยใช้ชื่อกลุ่มเป็น Key และรายการ AudioClip ของกลุ่มนั้นเป็น Value
            soundDictionary[soundEffectGroup.name] = soundEffectGroup.audioClips;
        }
    }

    public AudioClip GetRandomClip(string name)
    {
        // ตรวจสอบว่า soundDictionary มี Key (ชื่อกลุ่ม) ที่ระบุหรือไม่
        if (soundDictionary.ContainsKey(name))
        {
            // ถ้ามี ให้ดึงรายการ AudioClip ของกลุ่มนั้น
            List<AudioClip> audioClips = soundDictionary[name];

            // ตรวจสอบว่ามีคลิปเสียงในรายการหรือไม่
            if (audioClips != null && audioClips.Count > 0)
            {
                // ส่งคืนคลิปเสียงแบบสุ่มจากรายการ
                // UnityEngine.Random.Range(0, audioClips.Count) จะสร้างเลขสุ่มระหว่าง 0 (รวม) ถึง Count (ไม่รวม)
                return audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
            }
        }

        // หากไม่พบกลุ่มหรือกลุ่มว่างเปล่า ให้ส่งคืน null
        return null;
    }
}

// Struct สำหรับจัดกลุ่มคลิปเสียงด้วยชื่อ
[System.Serializable]
public struct SoundEffectGroup
{
    public string name; // ชื่อของกลุ่มเสียง
    public List<AudioClip> audioClips; // รายการของคลิปเสียงในกลุ่มนี้
}