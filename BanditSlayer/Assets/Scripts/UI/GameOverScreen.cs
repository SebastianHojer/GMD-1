using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Button restartButton;
    public void Setup()
    {
        gameObject.SetActive(true);
        SetFirstSelected(restartButton);
    }
    
    private void SetFirstSelected(Button button)
    {
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
    
    public void RestartButton()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("Town");
        gameObject.SetActive(false);
    }
    
    public void MainMenuButton()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("MainMenu");
        gameObject.SetActive(false);
    }
}
