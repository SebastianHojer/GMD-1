using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // Singleton
    public static CoinManager Instance { get; private set; }
    
    private int _balance = 0;
    
    public delegate void BalanceChangedEventHandler(int newBalance);
    public event BalanceChangedEventHandler OnBalanceChanged;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject persistent
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }
    
    public void AddToBalance(int amount)
    {
        _balance += amount;
        OnBalanceChanged?.Invoke(_balance);
    }
    
    public void DeductFromBalance(int amount)
    {
        _balance -= amount;
        OnBalanceChanged?.Invoke(_balance);
    }
    
    public int GetBalance()
    {
        return _balance;
    }
    
    public void ResetBalance()
    {
        _balance = 0;
        OnBalanceChanged?.Invoke(_balance);
    }
}
