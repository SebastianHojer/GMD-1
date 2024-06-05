using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    public class RoundManager : MonoBehaviour
    {
        public static RoundManager Instance { get; private set; }
        public GameObject GameOverScreen;
        public SceneReference BattlefieldSceneReference;
        public SceneReference TownSceneReference;
        public Action<float> OnRoundStart;
        public Action OnRoundOver;
        [SerializeField] private List<GameObject> enemyPrefabs;
        [SerializeField] private Transform[] spawnPoints;
        private float _spawnRate = 15f;
        private const float RoundDuration = 60f;
        private float _damageModifier = 0.25f;
        private bool _roundOver;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == BattlefieldSceneReference.sceneName)
            {
                StartCoroutine(RoundCoroutine());
                _roundOver = false;
            }
        }

        private IEnumerator RoundCoroutine()
        {
            if (_roundOver)
            {
                yield break;
            }
            System.Random rand = new System.Random();
            float roundEndTime = Time.time + RoundDuration;
            OnRoundStart?.Invoke(RoundDuration);
            while (Time.time < roundEndTime)
            {
                if(!_roundOver){
                    foreach (var spawnPoint in spawnPoints)
                    {
                        GameObject enemyPrefab = enemyPrefabs[rand.Next(enemyPrefabs.Count)];
                        var enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                        var enemy = enemyInstance.GetComponent<Enemy.Enemy>();
                        if (enemy)
                        {
                            enemy.SetDamageModifier(_damageModifier);   
                        }
                    }
                    yield return new WaitForSeconds(_spawnRate);
                }
            }

            while (GameObject.FindGameObjectWithTag("Enemy"))
            {
                yield return new WaitForSeconds(0.5f);
            }
            
            _damageModifier += 0.25f;
            if (_spawnRate > 2f)
            {
                _spawnRate -= 0.5f;
            }
            
            OnRoundOver.Invoke();

            Invoke(nameof(LoadTownScene), 3f);
        }

        private void LoadTownScene()
        {
            SceneManager.LoadScene(TownSceneReference.sceneName);
        }

        public void ResetRoundManager()
        {
            _damageModifier = 0.25f;
            _spawnRate = 15f;
            _roundOver = true;
            OnRoundOver.Invoke();
            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
        }
    }
}
