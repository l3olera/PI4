using Player47.Domain.Combat;
using UnityEngine;

namespace Player47.Domain.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] int _maxHealth = 100;
        int _currentHealth;

        void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            Debug.Log($"<color=green>Player perdeu dano e esta atualmente com: {_currentHealth}</color>");
            if (_currentHealth <= 0)
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