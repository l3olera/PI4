using UnityEngine;
using UnityEngine.AI;

namespace Player47.Domain.Enemy.States
{
    public class PatrolEnemyState : EnemyState
    {
        public PatrolEnemyState(GameObject enemy, NavMeshAgent agent, Animator animator, Transform player, Transform[] waypoints) : base(enemy, agent, animator, player, waypoints)
        {
            stateName = STATE.PATROL;
        }
    }
}