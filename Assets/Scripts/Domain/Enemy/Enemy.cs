using Player47.Domain.Combat;
using Player47.Domain.Enemy.States;
using Player47.Domain.Enemy.Vision;
using Player47.Domain.Player;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player47.Domain.Enemy
{
    [RequireComponent(typeof(AudioSource))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        public EnemyStatsSO stats;

        EnemyState _currentState;
        protected int _currentHealth;
        protected bool _isDead;

        [Header("Vision")]
        [SerializeField] protected IEnemyVision _vision;

        [Header("Components")]
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField] protected Animator _anim;
        [SerializeField] protected Collider _hitBox;

        [Header("Movements and Detection")]
        [SerializeField] protected Transform _player; // Referência a posição do jogador
        [SerializeField] protected Transform[] _waypoints; // Pontos de patrulha

        [Header("Audios")]
        [SerializeField] protected AudioSource _audioSource;
        public AudioSource AudioSource => _audioSource;


        [SerializeField] protected AudioClip _attackClip;
        [SerializeField] protected AudioClip _deathClip;
        [SerializeField] protected AudioClip[] _ambientClips; // Som ambiente padrão (grunhido)

        // Intervalo de tempo entre os sons (configurável)
        [SerializeField, Min(1f)] private float _ambientSoundInterval = 8f;

        public event Action<Enemy> OnDeath; // evento estático para notificar quando qualquer inimigo morrer    

        void Awake()
        {
            if (_agent == null)
                _agent = GetComponent<NavMeshAgent>();

            if (_anim == null)
                _anim = GetComponent<Animator>();

            if(_hitBox == null)
                _hitBox = GetComponent<Collider>();

            if(_audioSource == null)
                _audioSource = GetComponent<AudioSource>();
        }

        protected virtual void Start()
        {
            _currentHealth = stats.maxHealth;

            if (_player == null)
            {
                _player = PlayerController.Instance.transform;
            }

            EnsureAgentOnNavMesh();

            _currentState = new IdleEnemyState(gameObject, _agent, _anim, _player, _waypoints, stats.moveSpeed, stats.attackRange, stats.attackInterval); // Estado inicial genérico  
        }

        protected void EnsureAgentOnNavMesh()
        {
            if (_agent == null || !_agent.enabled || _agent.isOnNavMesh)
                return;

            int[] candidateAgentTypes = new int[] { _agent.agentTypeID, 1479372276, 0 };
            foreach (int agentType in candidateAgentTypes)
            {
                NavMeshQueryFilter filter = new NavMeshQueryFilter
                {
                    agentTypeID = agentType,
                    areaMask = NavMesh.AllAreas
                };

                if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5f, filter))
                {
                    if (_agent.agentTypeID != agentType)
                    {
                        _agent.agentTypeID = agentType;
                    }
                    _agent.Warp(hit.position);
                    if (_agent.isOnNavMesh)
                        return;
                }
            }
        }

        protected virtual void Update()
        {
            if (_isDead) return;

            _currentState = _currentState?.Process();

            // Chama o gerenciador central para tentar tocar som
            EnemyAmbientSoundManager.Instance.TryPlayAmbientSound(this, _ambientClips, _ambientSoundInterval);
        }

        public void TakeDamage(int amount)
        {
            if (_isDead) return;

            _currentHealth -= amount;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            _isDead = true;
            _currentState = null;

            if (_agent != null && _agent.isOnNavMesh)
            {
                _agent.isStopped = true; // Para de se mover
                _agent.ResetPath();
            }
            
            if (_hitBox != null)
                _hitBox.enabled = false; // Desativa o hitbox para evitar mais danos
            
            _anim.SetTrigger("isDying");

            if(_deathClip != null)
                _audioSource.PlayOneShot(_deathClip);

            EnemyManager.Instance.InvokeEnemyDied(this);
            OnDeath?.Invoke(this);  // dispara o evento de morte
        }

        // Essa função é chamada no final da animação pelo Animation Event
        public void OnDeathAnimationEnd()
        {
            Destroy(gameObject);    // destrói o inimigo só no final
        }
        
        public void PlaySoundAttack()
        {
            if (_attackClip == null) return;

            _audioSource.PlayOneShot(_attackClip);
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
