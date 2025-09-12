using UnityEngine;
using UnityEngine.AI;

namespace Player47.Domain.Enemy.States
{
    /// <summary>
    /// Estado de idle do inimigo
    /// 
    /// Responsável por parar o NavMeshAgent e tocar a animação de idle
    /// </summary>
    public class IdleEnemyState : EnemyState
    {
        float idleTime = 1f;

        public IdleEnemyState(GameObject enemy, NavMeshAgent agent, Animator animator, Transform player, Transform[] waypoints, float speed, float attackRange, float attackInterval) : base(enemy, agent, animator, player, waypoints, speed, attackRange, attackInterval)
        {
            stateName = STATE.IDLE;
        }

        public override void Enter()
        {
            _agent.isStopped = true;
            _animator.SetBool("isWalking", false);
            base.Enter();
        }

        public override void Update()
        {
            idleTime -= Time.deltaTime;

            if (idleTime <= 0f)
            {
                Debug.Log("Idle time ended, switching to Patrol");
                _nextState = new PatrolEnemyState(_enemy, _agent, _animator, _player, _waypoints, _speed,_attackRange, _attackInterval);
                _stage = EVENT.EXIT;
            }
        }
        public override void Exit()
        {
            Debug.Log("Saindo do Idle");
            base.Exit();
        }
    }
}