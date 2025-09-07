using System;
using UnityEngine;
using UnityEngine.AI;
using Player47.Domain.Combat;
using Player47.Domain.Enemy.States;

namespace Player47.Domain.Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [Serializable]
        public struct Stats
        {
            public int maxHealth;
            public float damage;
            public float damageInterval;
            public float moveSpeed;
            public int soulsToDrop;
        }

        [SerializeField] protected Stats _stats;
        EnemyState _currentState;
        protected int _currentHealth;

        [Header("Components")]
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField] protected Animator _anim;

        [Header("Movements and Detection")]
        [SerializeField] protected Transform _player; // Referência a posição do jogador
        [SerializeField] protected Transform[] _waypoints; // Pontos de patrulha

        public static event Action OnEnemyDied; // evento estático para notificar quando qualquer inimigo morrer    

        void Awake()
        {
            if (_agent == null)
                _agent = GetComponent<NavMeshAgent>();
            if (_anim == null)
                _anim = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            _currentHealth = _stats.maxHealth;
            _currentState = new IdleEnemyState(gameObject, _agent, _anim, _player, _waypoints); // Estado inicial genérico  
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

        public void Attack()
        {

        }

        protected virtual void Die()
        {
            _anim.SetTrigger("isDying");
            OnEnemyDied?.Invoke();  // dispara o evento de morte
            //_agent.isStopped = true; //Para de se mover
        }

        // Essa função é chamada no final da animação pelo Animation Event
        public void OnDeathAnimationEnd()
        {
            Destroy(gameObject);    // destrói o inimigo só no final
        }

        public void CanSeePlayer()
        {

        }
    }
}
