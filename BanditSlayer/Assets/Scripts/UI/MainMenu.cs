using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Town");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
}
