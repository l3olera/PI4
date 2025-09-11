using UnityEngine;
using UnityEngine.AI;

namespace Player47.Domain.Enemy.States
{
    public class AttackEnemyState : EnemyState
    {
        public AttackEnemyState(GameObject enemy, NavMeshAgent agent, Animator animator, Transform player, Transform[] waypoints, float speed, float attackRange, float attackInterval) : base(enemy, agent, animator, player, waypoints, speed, attackRange, attackInterval)
        {
            stateName = STATE.ATTACK;
            _agent.isStopped = true;
            
        }

        public override void Enter()
        {
            if(Time.time >= _lastAttackTime + _attackInterval)
            {
                _animator.SetTrigger("isAttacking");
                _lastAttackTime = Time.time;
                base.Enter();
            }
        }

        public override void Update()
        {
            if (_classEnemy.CanSeePlayer())
            {
                _nextState = new ChaseEnemyState(_enemy, _agent, _animator, _player, _waypoints, _speed, _attackRange, _attackInterval);
                _stage = EVENT.EXIT;
            }
            else
            {
                _nextState = new IdleEnemyState(_enemy, _agent, _animator, _player, _waypoints, _speed, _attackRange, _attackInterval);
                _stage = EVENT.EXIT;
            }
        }

        public override void Exit() {
            _animator.ResetTrigger("isAttacking");
            base.Exit();
        }
    }
}