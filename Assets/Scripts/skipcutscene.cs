using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    public string nextSceneName;

    // ฟังก์ชันนี้จะถูกเรียกเมื่อผู้เล่นคลิกปุ่ม
    public void SkipCutscene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}