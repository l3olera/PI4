using UnityEngine;
using UnityEngine.AI;

namespace Player47.Domain.Enemy.States
{
    public class AttackEnemyState : EnemyState
    {
        public AttackEnemyState(GameObject enemy, NavMeshAgent agent, Animator animator, Transform player, Transform[] waypoints, float speed, float attackRange, float attackInterval) : base(enemy, agent, animator, player, waypoints, speed, attackRange, attackInterval)
        {
            stateName = STATE.ATTACK;
        }

        public override void Enter()
        {
            StopAgent();
            //Debug.Log("Entrou no Attack");
            base.Enter();
        }

        public override void Update()
        {
            if(!_classEnemy.CanSeePlayer())
            {// Volta para Idle se não consegue ver o player
                _nextState = new IdleEnemyState(_enemy, _agent, _animator, _player, _waypoints, _speed, _attackRange, _attackInterval);
                _stage = EVENT.EXIT;
                return;
            }


            float distanceToPlayer = Vector3.Distance(_enemy.transform.position, _player.position);

            if (distanceToPlayer > _attackRange)
            { // Volta para Chase se o player sair do range de ataque
                _nextState = new ChaseEnemyState(_enemy, _agent, _animator, _player, _waypoints, _speed, _attackRange, _attackInterval);
                _stage = EVENT.EXIT;
                return;
            }

            Vector3 lookDirection = _player.position - _enemy.transform.position;
            lookDirection.y = 0f;
            if (lookDirection.sqrMagnitude > 0.001f)
            {
                _enemy.transform.rotation = Quaternion.Slerp(
                    _enemy.transform.rotation,
                    Quaternion.LookRotation(lookDirection),
                    Time.deltaTime * 5f
                );
            }
        
            if (Time.time >= _lastAttackTime + _attackInterval)
            {
                StopAgent();
                _animator.SetTrigger("isAttacking");
                _lastAttackTime = Time.time;
            }
        }

        public override void Exit() {
            _animator.ResetTrigger("isAttacking");
            base.Exit();
        }
    }
}