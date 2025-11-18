using UnityEngine;

namespace Player47.Domain.Enemy.Vision
{
    public class NormalVision : IEnemyVision
    {
        public bool CanSeePlayer(Transform enemy, Transform player, EnemyStatsSO stats)
        {
            EnemyManager.Instance.InvokeEnemySawPlayer(stats.idEnemy);
            if (stats.canAlwaysSee) return true;
            

            Vector3 direction = player.position - enemy.position;
            float angle = Vector3.Angle(direction, enemy.forward);
            return direction.magnitude < stats.visionRange && angle < stats.visionAngle;
        }
    }
}