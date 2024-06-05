using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    public class RoundManager : MonoBehaviour
    {
        public static RoundManager Instance { get; private set; }
        public SceneReference BattlefieldSceneReference;
        public Action<float> OnRoundStart;
        [SerializeField] private List<GameObject> enemyPrefabs;
        [SerializeField] private Transform[] spawnPoints;
        private float _spawnRate = 10f;
        private const float RoundDuration = 60f;
        private float _damageModifier = 0.5f;
        
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
            Debug.Log("Scene loaded: " + scene.name);
            if(scene.name==BattlefieldSceneReference.sceneName)
                StartCoroutine(RoundCoroutine());
        }

        private IEnumerator RoundCoroutine()
        {
            System.Random rand = new System.Random();
            float roundEndTime = Time.time + RoundDuration;
            OnRoundStart?.Invoke(RoundDuration);
            while (Time.time < roundEndTime)
            {
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

            while (GameObject.FindGameObjectWithTag("Enemy"))
            {
                yield return new WaitForSeconds(0.5f);
            }
            
            _damageModifier += 0.25f;
            if (_spawnRate > 2f)
            {
                _spawnRate -= 0.5f;
            }
        }
    }
}
