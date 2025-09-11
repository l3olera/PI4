using UnityEngine;

namespace Player47.Domain.Enemy.Weapon
{
    public class EnemyWeaponController : MonoBehaviour
    {
        public EnemyWeaponHitBox weaponHitBox;

        public void EnableWeaponHitBox()
        {
            if(weaponHitBox == null) return;

            Debug.Log("Ativando hitbox da arma do inimigo");
            weaponHitBox.EnableDamage();
        }

        public void DisableWeaponHitBox()
        {
            if (weaponHitBox == null) return;

            weaponHitBox.DisableDamage();
        }
    }

}