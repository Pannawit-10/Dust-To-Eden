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

    [SerializeField] private AudioSource audioSource; // ���� AudioSource ����������§�ҡ��

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    // ��Ǩ�ͺ��������� dialoguePanel �١��͹������������
    void Awake()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        // ��� AudioSource �ѧ�����١��˹����� Inspector ���������Ѻ�ҡ GameObject ���
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public bool CanInteract()
    {
        // ����ö��ͺ���������ͺ�ʹ����������ѧ�ӧҹ����
        return !isDialogueActive;
    }

    public void Interact()
    {
        // �������բ����ź�ʹ��� �������١��ش���Ǥ�����к�ʹ����������ѧ�ӧҹ���� �����ش
        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
            return;

        if (!isDialogueActive) // ��Һ�ʹ����ѧ�������� ����������ʹ���
        {
            StartDialogue();
        }
        else // ��Һ�ʹ��ҡ��ѧ�ӧҹ���� �������͹��ѧ��÷Ѵ�Ѵ� ���͢�����þ����
        {
            NextLine();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0; // ������鹷���÷Ѵ�á

        // ��˹���������ٻ�Ҿ�ͧ NPC
        nameText.SetText(dialogueData.npcName); // ��䢨ҡ npcname �� npcName (��� NPCDialogue)
        PortraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive(true); // �ʴ�ἧ��ʹ���
        PauseController.SetPause(true); // ��ش�����Ǥ���

        StartCoroutine(TypeLine()); // ������鹾�����÷Ѵ�á
    }

    void NextLine()
    {
        if (isTyping)
        {
            // ��ҡ��ѧ��������� �����ش�͹�����蹡�þ��������ʴ���ͤ��������÷Ѵ�ѹ��
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            // ����պ�÷Ѵ�Ѵ� ��������������÷Ѵ�Ѵ�
            StartCoroutine(TypeLine());
        }
        else
        {
            // �������պ�÷Ѵ������������� ��騺��ʹ���
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText(""); // ��ҧ��ͤ�������͡��͹����������

        // ������§�ҡ������Ѻ��÷Ѵ�Ѩ�غѹ
        if (audioSource != null && dialogueData.voicsound != null)
        {
            audioSource.volume = dialogueData.voiceVolume;
            audioSource.PlayOneShot(dialogueData.voicsound); // �����Ẻ one-shot �������������§��͹�ѹ
        }

        // ����� ��������ѡ�÷��е��
        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            SoundEffectManager.PlayVoice("dialogueData.voiceSound, dialogueData.voicePicth"); // ������§�����
            yield return new WaitForSeconds(dialogueData.dialogueDuration); // ��䢨ҡ typingSpeed �� dialogueDuration
        }

        isTyping = false;

        // ��Ǩ�ͺ�������͹��ʹ����ѵ��ѵ�
        // ��Ǩ�ͺ��������� array autoProgressLines �դ��������§�� ��к�÷Ѵ�Ѩ�غѹ�١��駤���������͹�ѵ��ѵ�
        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex]) // ��䢨ҡ DialogueData �� dialogueData
        {
            yield return new WaitForSeconds(dialogueData.autoProgessFDelay); // ��䢨ҡ autoProgressDelay �� autoProgessFDelay
            NextLine(); // ����͹��ѧ��÷Ѵ�Ѵ����ѵ��ѵ�
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines(); // ��ش���طչ�����ѧ�ӧҹ���������
        isDialogueActive = false; // ��駤��ʶҹк�ʹ��������ӧҹ
        dialogueText.SetText(""); // ��ҧ��ͤ�����ʹ���
        dialoguePanel.SetActive(false); // ��͹ἧ��ʹ���
        PauseController.SetPause(false); // ¡��ԡ�����ش�����Ǥ���
    }
}