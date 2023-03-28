using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject BiziDegerlendirPanel;
    private float rastgeleSayi;
    public void StartGame()
    {
        GameManager.distance = 0;
        // "Game" sahnesine geçiş yap
        Time.timeScale = 1;
        SceneManager.LoadScene("Loading");
    }

    public void QuitGame()
    {
        // Oyundan çıkış yap
        Application.Quit();
    }
    public void Home()
    {
       
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void Pause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }
    public void MoreGames(){
        Application.OpenURL("https://www.pumagame.com/");
    }
    public void Next()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void RateUs()
    {
        BiziDegerlendirPanel.SetActive(false);
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.PumaGames.ShadowNinjaRun");
    }
    public void RateUsExit()
    {
        BiziDegerlendirPanel.SetActive(false);
    }
}
