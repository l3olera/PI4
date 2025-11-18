using Player47.Domain.Player;
using UnityEngine;

namespace Player47.Domain.Enemy.Vision
{
    public class TorchFearVision : IEnemyVision
    {
        public bool CanSeePlayer(Transform enemy, Transform player, EnemyStatsSO stats)
        {
            if (TorchController.TorchEquippedValue) return false;

            return new NormalVision().CanSeePlayer(enemy, player, stats);
        }
    }
}