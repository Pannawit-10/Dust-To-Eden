using UnityEngine;
[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogus : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public float autoProgessFDelay = 1.5f;
    public float dialogueDuration = 0.05f;
    public AudioClip voicsound;
    public float voiceVolume = 1.0f;

}
