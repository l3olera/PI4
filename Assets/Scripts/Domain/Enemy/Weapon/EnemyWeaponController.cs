using UnityEngine;

namespace Player47.Domain.Enemy.Weapon
{
    public class EnemyWeaponController : MonoBehaviour
    {
        public EnemyWeaponHitBox weaponHitBox;

        public void DealDamage()
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        }

        public void EnableWeaponHitBox()
        {
            if(weaponHitBox == null) return;

            weaponHitBox.EnableDamage();
        }

        public void DisableWeaponHitBox()
        {
            if (weaponHitBox == null) return;

            weaponHitBox.DisableDamage();
        }
    }

}