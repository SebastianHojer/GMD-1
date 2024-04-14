using Collectables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BalanceDisplay : MonoBehaviour
{
    public TMP_Text balanceText;
    public TMP_SpriteAsset spriteAsset;

    void Start()
    {
        balanceText = GetComponent<TMP_Text>();
        if (balanceText == null)
        {
            Debug.LogError("TMP_Text component not assigned.");
            return;
        }

        // Subscribe to the balance changed event
        CoinManager.Instance.OnBalanceChanged += UpdateBalanceText;

        // Initial update of balance text
        UpdateBalanceText(CoinManager.Instance.GetBalance());
    }

    void UpdateBalanceText(int newBalance)
    {
        string balanceString = "<sprite name=\"bronze_coin\"> " + newBalance;
        balanceText.text = balanceString;
    }

    void OnDestroy()
    {
        if (CoinManager.Instance != null)
            CoinManager.Instance.OnBalanceChanged -= UpdateBalanceText;
    }
}
