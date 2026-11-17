using Player47.Domain.Enemy.Vision;
using UnityEngine;

namespace Player47.Domain.Enemy.Types
{
    public class OgreEnemy : Enemy
    {
        [Header("Boss Activation")]
        [SerializeField] private bool _startActive = false;
        private bool _isActive;

        protected override void Start()
        {
            int hittableLayer = LayerMask.NameToLayer("Hittable");
            if (hittableLayer != -1)
            {
                gameObject.layer = hittableLayer;
            }

            base.Start();

            _vision = new NormalVision();
            _isActive = _startActive;

            if (!_isActive && _agent != null && _agent.isOnNavMesh)
            {
                _agent.isStopped = true;
            }
        }

        protected override void Update()
        {
            if (!_isActive) return;

            base.Update();
        }

        public void ActivateBoss()
        {
            EnsureAgentOnNavMesh();
            _isActive = true;
            if (_anim != null)
            {
                _anim.speed = 1f;
            }
        }
    }
}

