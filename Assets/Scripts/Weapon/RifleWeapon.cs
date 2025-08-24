using UnityEngine;

namespace MidiaAdapt.Weapon
{
    public class RifleWeapon : WeaponBase
    {
        float _nextTimeToFire = 0f;

        public override void Shoot()
        {
            if (_currentAmmo > 0)
            {
                Debug.Log("Rifle fired");
                if (Time.time >= _nextTimeToFire)
                {
                    _nextTimeToFire = Time.time + 1f / _fireRate;
                    Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
                    _currentAmmo--;
                }
            }
        }

        public override void Reload()
        {
            base.Reload();
        }
    }
}
