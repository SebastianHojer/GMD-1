using System.Collections;
using System.Collections.Generic;
using GameLogic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance { get; private set; }
    public GameOverScreen GameOverScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void CheckPlayerDeath()
    {
        if (Player.PlayerController.Instance.PlayerIsDead())
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void ResetGame()
    {
        Player.PlayerController.Instance.ResetPlayer();
        CoinManager.Instance.ResetBalance();
        UI.Shop_UI.Instance.ResetShop();
        RoundManager.Instance.ResetRoundManager();
    }
}