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
        float idleTime = 2f;

        public IdleEnemyState(GameObject enemy, NavMeshAgent agent, Animator animator, Transform player, Transform[] waypoints) : base(enemy, agent, animator, player, waypoints)
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
            Debug.Log("Idle... " + idleTime);
            if (idleTime <= 0f)
            {
                _nextState = new PatrolEnemyState(_enemy, _agent, _animator, _player, _waypoints);
                _stage = EVENT.EXIT;
            }
            base.Update();
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}