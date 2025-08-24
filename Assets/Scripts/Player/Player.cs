using UnityEngine;
using MidiaAdapt.Core.Interfaces;

public class Player : MonoBehaviour, IDamageable
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

        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died");
    }
}
