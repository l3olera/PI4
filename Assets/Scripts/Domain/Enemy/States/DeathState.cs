using UnityEngine;

namespace Player47.Domain.Enemy.States
{
    public class DeathState : StateMachineBehaviour
    {
        /// <summary>
        /// Disparado quando a animação de morte do inimigo termina.
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.TryGetComponent<Enemy>(out Enemy enemy))
            {
                Debug.Log("Enemy death animation ended.");
                enemy.OnDeathAnimationEnd();
            }
        }
    }

}