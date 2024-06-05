using UnityEngine;

namespace GameLogic
{
    public class CanvasManager : MonoBehaviour
    {
        private static CanvasManager _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
    
