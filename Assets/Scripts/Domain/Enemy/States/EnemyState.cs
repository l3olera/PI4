using Player47.Core.State;
using UnityEngine;
using UnityEngine.AI;

namespace Player47.Domain.Enemy
{
    /// <summary>
    /// Método base para estados de inimigos
    /// 
    /// Possui enums para nome do estado e eventos (entrar, atualizar, sair)
    /// 
    /// Os estados específicos (Idle, Patrol, Chase, Attack) irão herdar dessa classe
    /// 
    /// Todos os estados terão referência ao inimigo, NavMeshAgent e Animator
    /// </summary>
    public class EnemyState : IState
    {
        public enum STATE { IDLE, PATROL, CHASE, ATTACK }
        public enum EVENT { ENTER, UPDATE, EXIT }

        public STATE stateName;
        protected EVENT _stage;
        protected EnemyState _nextState;
        protected Enemy _classEnemy;
        protected float _lastAttackTime;

        protected GameObject _enemy;
        protected NavMeshAgent _agent;
        protected Animator _animator;
        protected Transform _player;
        protected Transform[] _waypoints; 
        protected float _speed;
        protected float _attackRange;
        protected float _attackInterval;

        protected void StopAgent()
        {
            if (_agent != null)
            {
                _agent.acceleration = 0;
                _agent.isStopped = true;
            }
        }

        protected void StartAgent()
        {
            if (_agent != null)
            {
                _agent.acceleration = _classEnemy.stats.acceleration;
                _agent.isStopped = false;
            }
        }

        public EnemyState(GameObject enemy, NavMeshAgent agent, Animator animator, Transform player, Transform[] waypoints, float speed, float attackRange, float attackInterval)
        {
            _enemy = enemy;
            _agent = agent;
            _animator = animator;
            _player = player;
            _waypoints = waypoints;
            _speed = speed;
            _attackRange = attackRange;
            _attackInterval = attackInterval;

            if (_enemy.TryGetComponent<Enemy>(out Enemy e))
                _classEnemy = e;
        }

        public virtual void Enter()
        {
            _stage = EVENT.UPDATE;
        }

        public virtual void Update()
        {
            _stage = EVENT.UPDATE;
        }

        public virtual void Exit()
        {
            _stage = EVENT.EXIT;
        }

        /// <summary>
        /// Chamado na Update do inimigo, processa o estado atual
        /// </summary>
        /// <returns>Qual o estado atual do processo</returns>
        public EnemyState Process()
        {
            switch (_stage) {                 
                case EVENT.ENTER:
                    Enter();
                    break;
                case EVENT.UPDATE:
                    Update();
                    break;
                case EVENT.EXIT:
                    Exit();
                    return _nextState;
                default:
                    break;
            }

            return this;
        }
    }

}