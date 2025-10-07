using Player47.Domain.Combat;
using Player47.Domain.Enemy.States;
using Player47.Domain.Enemy.Vision;
using Player47.Domain.Player;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player47.Domain.Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public EnemyStatsSO stats;

        EnemyState _currentState;
        protected int _currentHealth;


        [Header("Vision")]
        [SerializeField] protected IEnemyVision _vision;

        [Header("Components")]
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField] protected Animator _anim;
        [SerializeField] protected BoxCollider _hitBox;

        [Header("Movements and Detection")]
        [SerializeField] protected Transform _player; // Referência a posição do jogador
        [SerializeField] protected Transform[] _waypoints; // Pontos de patrulha

        public event Action<Enemy> OnDeath; // evento estático para notificar quando qualquer inimigo morrer    

        void Awake()
        {
            if (_agent == null)
                _agent = GetComponent<NavMeshAgent>();

            if (_anim == null)
                _anim = GetComponent<Animator>();

            if(_hitBox == null)
                _hitBox = GetComponent<BoxCollider>();
        }

        protected virtual void Start()
        {
            _currentHealth = stats.maxHealth;

            if (_player == null)
            {
                _player = PlayerController.Instance.transform;
            }

            _currentState = new IdleEnemyState(gameObject, _agent, _anim, _player, _waypoints, stats.moveSpeed, stats.attackRange, stats.attackInterval); // Estado inicial genérico  
        }

        protected virtual void Update()
        {
            _currentState = _currentState?.Process();
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            Debug.Log($"{gameObject.name} took {amount} damage. Current health: {_currentHealth}");

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            _agent.isStopped = true; // Para de se mover
            
            _hitBox.enabled = false; // Desativa o hitbox para evitar mais danos
            
            _anim.SetTrigger("isDying");

            OnDeath?.Invoke(this);  // dispara o evento de morte
            EnemyManager.Instance.InvokeEnemyDied(this);
        }

        // Essa função é chamada no final da animação pelo Animation Event
        public void OnDeathAnimationEnd()
        {
            Destroy(gameObject);    // destrói o inimigo só no final
        }

        public void SetWaypoint(Transform[] waypoints)
        {
            _waypoints = waypoints;
        }

        public bool CanSeePlayer()
        {
            return _vision.CanSeePlayer(transform, _player, stats);
        }
    }
}
