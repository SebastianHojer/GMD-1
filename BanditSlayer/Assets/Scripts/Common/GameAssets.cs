using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;
    public static GameAssets Instance
    {
        get {
            if (_instance == null) _instance = (Instantiate(Resources.Load("GameAssets")) as GameObject)?.GetComponent<GameAssets>();
            return _instance;
        }
    }

    public GameObject daggerPrefab;
    public GameObject axePrefab;
    public GameObject katanaPrefab;
    public GameObject macePrefab;
    public GameObject polearmPrefab;
    public GameObject spearPrefab;
    public GameObject swordPrefab;
}


