using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("Town");
        GameManager.Instance.ResetGame();
    }
    
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        GameManager.Instance.ResetGame();
    }
}
