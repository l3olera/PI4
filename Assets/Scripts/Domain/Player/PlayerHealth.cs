using Player47.Core.Service;
using Player47.Domain.Combat;
using UnityEngine;

namespace Player47.Domain.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        public static PlayerHealth Instance { get; private set; }

        [SerializeField] int _maxHealth = 100;

        public int MaxHealth => _maxHealth;

        public Observer<int> CurrentHealth = new(1);

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        void Start()
        {
            CurrentHealth.Value = _maxHealth;
            CurrentHealth.Invoke();
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth.Value -= amount;
            Debug.Log($"<color=green>Player perdeu dano e esta atualmente com: {CurrentHealth.Value}</color>");
            if (CurrentHealth.Value <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Debug.Log("Player Died");
        }
    }
}