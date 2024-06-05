using System.Collections;
using UnityEngine;
using GameLogic;
using TMPro;

namespace UI
{
    public class TimeDisplay : MonoBehaviour
    {
        private TextMeshProUGUI timeText;
        private RoundManager roundManager;
        private float countdown;
        private bool roundStarted;
        private bool roundOver;

        private void Awake()
        {
            timeText = GetComponent<TextMeshProUGUI>();
            roundManager = RoundManager.Instance;
            roundManager.OnRoundStart += StartCountdown;
            roundManager.OnRoundOver += RoundOver;
        }

        private void StartCountdown(float duration)
        {
            roundStarted = true;
            roundOver = false;
            countdown = duration;
        }

        private void Update()
        {
            if (countdown > 0 && roundStarted && !roundOver)
            {
                countdown -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(countdown / 60F);
                int seconds = Mathf.FloorToInt(countdown - minutes * 60);
                string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

                timeText.text = niceTime;
            }
            else if(roundStarted && !roundOver)
            {
                timeText.text = "Defeat the remaining enemies!";
            }
        }

        private void RoundOver()
        {
            roundOver = true;
            timeText.text = "All enemies defeated! Returning to town.";
            StartCoroutine(RemoveTextAfterDelay(2));
        }
        
        private IEnumerator RemoveTextAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            timeText.text = "";
        }
    }
}