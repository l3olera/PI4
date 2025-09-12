using Player47.Domain.Combat;
using UnityEngine;

namespace Player47.Domain.Enemy.Weapon
{
    public class EnemyWeaponHitBox : MonoBehaviour
    {
        public int damage;

        bool _canDamage;//Controla se o inimigo pode causar dano ou não
        bool _hasHit; //Para evitar que o inimigo acerte mais de uma vez no mesmo ataque

        void Start()
        {
            GetComponentInParent<EnemyWeaponController>().weaponHitBox = this;  //Instancia o hitbox no seu controlador
        }

        void OnTriggerStay(Collider other)
        {
            if (_canDamage && other.CompareTag("Player")){ 
                if(_hasHit) return; //Se já acertou, não acerta de novo
                if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                    _hasHit = true; //Marca que já acertou
                }
            }
        }

        //É chamado no começo da animação de ataque
        public void EnableDamage()
        {
            _canDamage = true;
            _hasHit = false;
        }

        //É chamado no fim da animação de ataque
        public void DisableDamage()
        {
            _canDamage = false;
        }
    } 
}
