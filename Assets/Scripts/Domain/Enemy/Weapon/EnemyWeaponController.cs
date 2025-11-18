using UnityEngine;

namespace Player47.Domain.Enemy.Weapon
{
    public class EnemyWeaponController : MonoBehaviour
    {
        public EnemyWeaponHitBox weaponHitBox;

        Enemy m_enemy;

        void Awake()
        {
            m_enemy = GetComponent<Enemy>();
        }

        public void EnableWeaponHitBox()
        {
            if(weaponHitBox == null) return;

            m_enemy.PlaySoundAttack();

            weaponHitBox.EnableDamage();
        }

        public void DisableWeaponHitBox()
        {
            if (weaponHitBox == null) return;

            weaponHitBox.DisableDamage();
        }
    }

}