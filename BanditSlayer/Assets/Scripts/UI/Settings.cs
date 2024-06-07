using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider fxSlider;
    
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void SetMusicVolume()
    {
        Debug.Log("Music volume: " + musicSlider.value);
        AudioManager.Instance.SetMusicVolume(musicSlider.value);
    }
    
    public void SetFxVolume()
    {
        Debug.Log("FX volume: " + fxSlider.value);
        AudioManager.Instance.SetFXVolume(fxSlider.value);
    }
}
