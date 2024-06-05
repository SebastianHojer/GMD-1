using UnityEngine;
using UnityEngine.UI;
using GameLogic;
using TMPro;

namespace UI
{
    public class TimeDisplay : MonoBehaviour
    {
        private TextMeshProUGUI timeText;
        private RoundManager roundManager;
        private float countdown;

        private void Awake()
        {
            timeText = GetComponent<TextMeshProUGUI>();
            roundManager = RoundManager.Instance;
            roundManager.OnRoundStart += StartCountdown;
        }

        private void StartCountdown(float duration)
        {
            countdown = duration;
        }

        private void Update()
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(countdown / 60F);
                int seconds = Mathf.FloorToInt(countdown - minutes * 60);
                string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

                timeText.text = niceTime;
            }
        }
    }
}