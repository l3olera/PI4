using UnityEngine;
using UnityEngine.AI;

namespace Player47.Domain.Enemy.States
{
    public class ChaseEnemyState : EnemyState
    {
        public ChaseEnemyState(GameObject enemy, NavMeshAgent agent, Animator animator, Transform player, Transform[] waypoints, float speed, float attackRange, float attackInterval) : base(enemy, agent, animator, player, waypoints, speed, attackRange, attackInterval)
        {
            stateName = STATE.CHASE;
            _agent.speed = _speed * 2;
        }

        public override void Enter()
        {
            StartAgent();
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
                        _nextState = new AttackEnemyState(_enemy, _agent, _animator, _player, _waypoints, _speed, _attackRange, _attackInterval);
                        _agent.isStopped = true;
                        _stage = EVENT.EXIT;
                    }
                }
                else
                {
                    _nextState = new IdleEnemyState(_enemy, _agent, _animator, _player, _waypoints, _speed, _attackRange, _attackInterval);
                    _stage = EVENT.EXIT;
                }
            }
        }

        public override void Exit()
        {
            Debug.Log("Entrou no Exit do Chase");
            _animator.SetBool("isWalking", false);
            base.Exit();
        }
    }
}