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

    public Sprite daggerSprite;
    public Sprite axeSprite;
    public Sprite katanaSprite;
    public Sprite maceSprite;
    public Sprite polearmSprite;
    public Sprite rodSprite;
    public Sprite spearSprite;
    public Sprite staffSprite;
    public Sprite swordSprite;
}


