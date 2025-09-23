using UnityEngine;
using Player47.Domain.Souls;

namespace Player47.Domain.Enemy
{
    /// <summary>
    /// Adiciona recompensas ao jogador quando inimigos morrem.
    /// 
    /// Nesse sistema, quando um inimigo morre, ele notifica o <see cref="SoulsManager"> para adicionar almas ao total do jogador.
    /// </summary>
    public class EnemyRewardSystem : MonoBehaviour
    {
        void OnEnable()
        {
            Enemy.EnemyDied += Enemy_EnemyDied;
        }

        void OnDisable()
        {
            Enemy.EnemyDied -= Enemy_EnemyDied;
        }

        void Enemy_EnemyDied(Enemy enemy)
        {
            SoulsManager.Instance.AddSouls(enemy.stats.soulReward);
        }
    }

}