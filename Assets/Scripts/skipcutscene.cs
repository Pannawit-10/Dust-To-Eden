using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    public string nextSceneName;

    // �ѧ��ѹ���ж١���¡����ͼ����蹤�ԡ����
    public void SkipCutscene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}