using UnityEngine;
using UnityEngine.AI;

namespace Player47.Domain.Enemy.States
{
    public class ChaseEnemyState : EnemyState
    {
        public ChaseEnemyState(GameObject enemy, NavMeshAgent agent, Animator animator, Transform player, Transform[] waypoints, float speed, float attackRange) : base(enemy, agent, animator, player, waypoints, speed, attackRange)
        {
            stateName = STATE.CHASE;
            _agent.speed = _speed * 2;
            _agent.isStopped = false;
        }

        public override void Enter()
        {
            _animator.SetBool("isWalking", true);
            base.Enter();
        }

        public override void Update()
        {
            _agent.SetDestination(_player.position);

            if (_agent.hasPath)
            {
                if (_classEnemy.CanSeePlayer())
                {
                    if(Vector3.Distance(_enemy.transform.position, _player.position) <= _attackRange)
                    {
                        //_nextState = new AttackEnemyState(_enemy, _agent, _animator, _player, _waypoints, _speed);
                        //_stage = EVENT.EXIT;
                    }
                }
                else
                {
                    _nextState = new IdleEnemyState(_enemy, _agent, _animator, _player, _waypoints, _speed, _attackRange);
                    _stage = EVENT.EXIT;
                }
            }
        }

        public override void Exit()
        {
            _animator.SetBool("isWalking", false);
            base.Exit();
        }
    }
}