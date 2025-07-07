//using System.Collections;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class NPC : MonoBehaviour, IInteractable
//{
//    public NPCDialogus DialogusData;
//    public GameObject DialoguePanel;
//    public TMP_Text DialogueText, nameText;
//    public Image PortraitImage;

//    private int DialogueIndex;
//    private bool isTyping, isDialogueActive;

//    public bool CanInteract()
//    {
//        return !isDialogueActive;
//    }

//    public void Interact()
//    {
//        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
//            return;

//        if (!isDialogueActive)
//        {
//            NextLine();
//        }
//        else
//        {
//            StartDialogue();
//        }
//    }

//    void StartDialogue()
//    {
//        isDialogueActive = true;
//        DialogueIndex = 0;

//        nameText.SetText(dialogueData.name);
//        PortraitImage.sprite = dialogueData.npcPortrait;

//        DialoguePanel.SetActive(true);
//        PauseController.SetPause(true);

//        StartCoroutine(TypeLine());
//    }

//    void NextLine()
//    {
//        if (isTyping)
//        {
//            StopAllCoroutines();
//            DialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
//            isTyping = false;
//        }
//        else if (++DialogueIndex < dialogueData.dialogueLines.Length)
//        {
//            //if
//            StartCoroutine(TypeLine());
//        }
//        else
//        {
//            EndDialogue();
//        }
//    }

//    IEnumerator TypeLine()
//    {
//        isTyping = true;
//        DialogueText.SetText("");

//        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
//        {
//            DialogueText.text += letter;
//            yield return new WaitForSeconds(dialogueData.typingSpeed);
//        }

//        isTyping = false;

//        if (dialogueData.autoProgressLines.Length > DialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
//        {
//            yield return new WaitForSeconds(DialogueData.autoProgressDelay);
//            NextLine();
//        }
//    }

//    public void EndDialogue()
//    {
//        StopAllCoroutines();
//        isDialogueActive = false;
//        DialogueText.SetText("");
//        DialoguePanel.SetActive(false);
//        PauseController.SetPause(false);
//    }

//}


