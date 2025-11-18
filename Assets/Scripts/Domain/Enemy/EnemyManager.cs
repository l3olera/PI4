using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player47.Domain.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance { get; private set; }
        public static event Action<Enemy> EnemyDied; // evento estático para notificar quando qualquer inimigo 
        public UnityEvent<IdEnemy> EnemySawPlayer = new(); // evento estático para notificar quando qualquer inimigo é visto

        void Awake()
        {
            if (Instance == null) 
            { 
                Instance = this;
            }
            else
            {
                Destroy(Instance);
            }
        }

        public void InvokeEnemyDied(Enemy enemy)
        {
            EnemyDied?.Invoke(enemy);
        }

        public void InvokeEnemySawPlayer(IdEnemy idEnemy)
        {
            EnemySawPlayer?.Invoke(idEnemy);
        }
    }
}