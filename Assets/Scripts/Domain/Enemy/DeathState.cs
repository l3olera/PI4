using UnityEngine;

namespace Player47.Domain.Enemy
{
    public class DeathState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.OnDeathAnimationEnd();
            }
        }
    }

}