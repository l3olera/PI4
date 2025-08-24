using UnityEngine;
using MidiaAdapt.Core.Interfaces;

namespace MidiaAdapt.Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] protected int _maxHealth = 100;
        protected int _currentHealth;

        protected virtual void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            
            if(_currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
