using UnityEngine;
using System.Collections;
using MidiaAdapt.Core.Interfaces;

namespace MidiaAdapt.Weapon
{
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {
        [SerializeField] protected GameObject _bulletPrefab;
        [SerializeField] protected Transform _firePoint;
        [SerializeField] protected float _range;
        [SerializeField] protected float _fireRate;
        [SerializeField] protected int _ammoCapacity;
        [SerializeField] protected float _reloadTime;
        protected int _currentAmmo;

        protected virtual void Start()
        {
            _currentAmmo = _ammoCapacity;
        }

        public abstract void Shoot();

        public virtual void Reload()
        {
            StartCoroutine(ReloadCourotine());
        }

        IEnumerator ReloadCourotine()
        {
            yield return new WaitForSeconds(_reloadTime);
            _currentAmmo = _ammoCapacity;
        }
    }
}
