using UnityEngine;

namespace Player47.Domain.Enemy.Vision
{
	public interface IEnemyVision
	{
        bool CanSeePlayer(Transform enemy, Transform player, EnemyStatsSO stats);
    }

}