using Player47.Core;
using Player47.Domain.Enemy;
using System;
using UnityEngine;

namespace Player47.Domain.Tutorials
{
    public class TutorialTriggerOnEnemySawPlayer : MonoBehaviour, IEventSource
    {
        public event Action Triggered;
        public IdEnemy enemyTarget;

        void OnEnable()
        {
            EnemyManager.Instance.EnemySawPlayer.AddListener(EnemyManager_EnemySawPlayer);
        }

        void OnDisable()
        {
            EnemyManager.Instance.EnemySawPlayer.RemoveListener(EnemyManager_EnemySawPlayer);
        }

        private void EnemyManager_EnemySawPlayer(IdEnemy id)
        {
            if(id == enemyTarget)
            {
                Triggered?.Invoke();
                OnDisable();
            }
        }
    }

}