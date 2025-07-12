//using UnityEngine;
//using UnityEngine.Audio;

//public class MainManu : MonoBehaviour
//{
//    public AudioMixer audioMixer; // Reference to the AudioMixer component
//    private void Start()
//    {
//        MusicManager.Instance.PlayMusic("MainMenu");
//    }
//    public void Play()
//    {
//        LevelManager.Instance.LoadScene("Game", "CrossFade");
//        MusicManager.Instance.PlayMusic("Game");
//    }
//    public void Quit()
//    {
//        Application.Quit();
//    }
//    public void UpdateMusicVolume(float volume)
//    {
//        audioMixer.SetFloat("MusicVolume", volume);
//    }
//    public void UpdateSoundVolume(float volume)
//    {
//        audioMixer.SetFloat("SFXVolume", volume);
//    }
