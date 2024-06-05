using System.Collections;
using System.Collections.Generic;
using Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    public SceneReference scene1;
    public SceneReference scene2;

    public void Interact(GameObject interactor)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == scene1.sceneName)
        {
            SceneManager.LoadScene(scene2.sceneName);
        }
        else if (currentScene == scene2.sceneName)
        {
            SceneManager.LoadScene(scene1.sceneName);
        }
    }
}
