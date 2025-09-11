using Player47.Domain.Combat;
using UnityEngine;

namespace Player47.Domain.Enemy
{
    public class EnemyWeaponHitBox : MonoBehaviour
    {
        public int damage;

        bool _canDamage;

        void OnTriggerEnter(Collider other)
        {
            if(_canDamage && other.CompareTag("Player")){
                if(other.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                }
            }
        }

        //É chamado no começo da animação de ataque
        public void EnableDamage()
        {
            _canDamage = true;
        }

        //É chamado no fim da animação de ataque
        public void DisableEnable()
        {
            _canDamage = false;
        }
    } 
}
