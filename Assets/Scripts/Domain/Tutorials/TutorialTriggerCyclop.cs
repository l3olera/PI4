using Player47.Core;
using Player47.Domain.Enemy;
using System;
using UnityEngine;

namespace Player47.Domain.Tutorials
{
    public class TutorialTriggerCyclop : MonoBehaviour, IEventSource
    {
        public event Action Triggered;

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
            if(id == IdEnemy.Cyclop)
            {
                Triggered?.Invoke();
                OnDisable();
            }
        }
    }

}