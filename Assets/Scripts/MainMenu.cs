using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Method to start the game
    public void StartGame()
    {
        SceneManager.LoadScene("cutscene01");
    }
    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }
    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
    public void backtomenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

