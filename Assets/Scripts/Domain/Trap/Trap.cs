using UnityEngine;
using Player47.Core.Service.Triggers;
using Player47.Domain.Combat;

namespace Player47.Domain.Trap
{
    public class Trap : TriggerSetup
    {
        public int damage;
        [SerializeField] Animator m_anim;

        bool m_isActivated;

        protected override void Awake()
        {
            base.Awake();
            if(m_anim == null)
            {
                m_anim = GetComponent<Animator>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (m_isActivated) return;

            if (other.CompareTag("Player") && other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                m_anim.SetTrigger("Close");
                m_isActivated = true;
                damageable.TakeDamage(damage);
            }
               
        }
    }
}
