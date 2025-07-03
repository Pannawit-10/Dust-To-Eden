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

//        if (DialogusData == null || (PauscController.IsGamePaused && !isDialogueActive))
//            return;    
//    }
//}
