using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image PortraitImage;

    [SerializeField] private AudioSource audioSource; // เพิ่ม AudioSource เพื่อเล่นเสียงพากย์

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    // ตรวจสอบให้แน่ใจว่า dialoguePanel ถูกซ่อนเมื่อเริ่มต้น
    void Awake()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        // ถ้า AudioSource ยังไม่ได้ถูกกำหนดค่าใน Inspector ให้พยายามรับจาก GameObject นี้
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public bool CanInteract()
    {
        // สามารถโต้ตอบได้ก็ต่อเมื่อบทสนทนาไม่ได้กำลังทำงานอยู่
        return !isDialogueActive;
    }

    public void Interact()
    {
        // ถ้าไม่มีข้อมูลบทสนทนา หรือเกมถูกหยุดชั่วคราวและบทสนทนาไม่ได้กำลังทำงานอยู่ ให้หยุด
        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
            return;

        if (!isDialogueActive) // ถ้าบทสนทนายังไม่เริ่ม ให้เริ่มบทสนทนา
        {
            StartDialogue();
        }
        else // ถ้าบทสนทนากำลังทำงานอยู่ ให้เลื่อนไปยังบรรทัดถัดไป หรือข้ามการพิมพ์
        {
            NextLine();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0; // เริ่มต้นที่บรรทัดแรก

        // กำหนดชื่อและรูปภาพของ NPC
        nameText.SetText(dialogueData.npcName); // แก้ไขจาก npcname เป็น npcName (ตาม NPCDialogue)
        PortraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive(true); // แสดงแผงบทสนทนา
        PauseController.SetPause(true); // หยุดเกมชั่วคราว

        StartCoroutine(TypeLine()); // เริ่มต้นพิมพ์บรรทัดแรก
    }

    void NextLine()
    {
        if (isTyping)
        {
            // ถ้ากำลังพิมพ์อยู่ ให้หยุดแอนิเมชั่นการพิมพ์และแสดงข้อความเต็มบรรทัดทันที
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            // ถ้ามีบรรทัดถัดไป ให้เริ่มพิมพ์บรรทัดถัดไป
            StartCoroutine(TypeLine());
        }
        else
        {
            // ถ้าไม่มีบรรทัดเหลืออยู่แล้ว ให้จบบทสนทนา
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText(""); // ล้างข้อความเก่าออกก่อนเริ่มพิมพ์

        // เล่นเสียงพากย์สำหรับบรรทัดปัจจุบัน
        if (audioSource != null && dialogueData.voicsound != null)
        {
            audioSource.volume = dialogueData.voiceVolume;
            audioSource.PlayOneShot(dialogueData.voicsound); // เล่นเป็นแบบ one-shot เพื่อไม่ให้เสียงซ้อนกัน
        }

        // ค่อยๆ พิมพ์ตัวอักษรทีละตัว
        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            SoundEffectManager.PlayVoice("dialogueData.voiceSound, dialogueData.voicePicth"); // เล่นเสียงพิมพ์
            yield return new WaitForSeconds(dialogueData.dialogueDuration); // แก้ไขจาก typingSpeed เป็น dialogueDuration
        }

        isTyping = false;

        // ตรวจสอบการเลื่อนบทสนทนาอัตโนมัติ
        // ตรวจสอบให้แน่ใจว่า array autoProgressLines มีความยาวเพียงพอ และบรรทัดปัจจุบันถูกตั้งค่าให้เลื่อนอัตโนมัติ
        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex]) // แก้ไขจาก DialogueData เป็น dialogueData
        {
            yield return new WaitForSeconds(dialogueData.autoProgessFDelay); // แก้ไขจาก autoProgressDelay เป็น autoProgessFDelay
            NextLine(); // เลื่อนไปยังบรรทัดถัดไปโดยอัตโนมัติ
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines(); // หยุดคอรุทีนที่กำลังทำงานอยู่ทั้งหมด
        isDialogueActive = false; // ตั้งค่าสถานะบทสนทนาเป็นไม่ทำงาน
        dialogueText.SetText(""); // ล้างข้อความบทสนทนา
        dialoguePanel.SetActive(false); // ซ่อนแผงบทสนทนา
        PauseController.SetPause(false); // ยกเลิกการหยุดเกมชั่วคราว
    }
}