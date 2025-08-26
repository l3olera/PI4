using UnityEngine;

namespace MidiaAdapt.Weapon
{
    public class RifleWeapon : WeaponBase
    {
        float _nextTimeToFire = 0f;

        public override void Shoot()
        {
            /*Lembrar de fazer o pool de balas depois e repense em fazer essa lógica na base da weapon*/
            if (_currentAmmo > 0 && !isReloading)
            {
                if (Time.time >= _nextTimeToFire)
                {
                    _nextTimeToFire = Time.time + 1f / _fireRate;
                    Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
                    RemoveAmmo(1);
                }
            }
        }

        public override void Reload()
        {
            base.Reload();
        }
    }
}
