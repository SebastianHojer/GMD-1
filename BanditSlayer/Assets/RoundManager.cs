using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnRate = 5f;
    private const float RoundDuration = 60f;
    private bool _isRoundActive;

    private void Start()
    {
        StartCoroutine(RoundCoroutine());
    }

    private IEnumerator RoundCoroutine()
    {
        _isRoundActive = true;

        float roundEndTime = Time.time + RoundDuration;
        while (Time.time < roundEndTime)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnRate);
        }

        _isRoundActive = false;

        while (GameObject.FindGameObjectWithTag("Enemy"))
        {
            yield return new WaitForSeconds(0.5f);
        }

        // Round is over, all enemies are destroyed
        // You can add code here to handle the end of the round
    }
}
