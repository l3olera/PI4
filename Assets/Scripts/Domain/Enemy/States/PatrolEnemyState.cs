using UnityEngine;
using UnityEngine.AI;

namespace Player47.Domain.Enemy.States
{
    public class PatrolEnemyState : EnemyState
    {
        int _currentIndex = -1;

        public PatrolEnemyState(GameObject enemy, NavMeshAgent agent, Animator animator, Transform player, Transform[] waypoints, float speed, float attackRange, float attackInterval) : base(enemy, agent, animator, player, waypoints, speed, attackRange, attackInterval)
        {
            stateName = STATE.PATROL;
            _agent.speed = _speed;
        }

        public override void Enter()
        {
            StartAgent();
            float lastDist = Mathf.Infinity;

            if (_waypoints != null)
            {
                for (int i = 0; i < _waypoints.Length; i++)
                {
                    if (_waypoints[i] == null) continue;

                    float dist = Vector3.Distance(_enemy.transform.position, _waypoints[i].position);
                    if (dist < lastDist)
                    {
                        _currentIndex = i - 1;
                        lastDist = dist;
                    }
                }
            }

            _animator.SetBool("isWalking", true);

            base.Enter();
        }

        public override void Update()
        {
            if (_agent != null && _agent.isActiveAndEnabled && _agent.isOnNavMesh && _waypoints != null && _waypoints.Length > 0 && !_agent.pathPending && _agent.remainingDistance < 1)
            {
                if (_currentIndex >= _waypoints.Length - 1)
                    _currentIndex = 0;
                else
                    _currentIndex++;

                if (_waypoints[_currentIndex] != null)
                    _agent.SetDestination(_waypoints[_currentIndex].position);
            }

            if (_classEnemy.CanSeePlayer())
            {
                _nextState = new ChaseEnemyState(_enemy, _agent, _animator, _player, _waypoints, _speed, _attackRange, _attackInterval);
                _stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            _animator.SetBool("isWalking", false);
            base.Exit();
        }
    }
}