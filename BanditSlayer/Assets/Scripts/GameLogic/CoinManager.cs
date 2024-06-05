using UnityEngine;

namespace GameLogic
{
    public class CoinManager : MonoBehaviour
    {
        // Singleton
        public static CoinManager Instance { get; private set; }
    
        private int _balance = 200;
    
        public delegate void BalanceChangedEventHandler(int newBalance);
        public event BalanceChangedEventHandler OnBalanceChanged;
    
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
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
        
        public bool SpendGoldAmount(int amount)
        {
            if (_balance >= amount)
            {
                DeductFromBalance(amount);
                return true; 
            }
            return false; 
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
}
