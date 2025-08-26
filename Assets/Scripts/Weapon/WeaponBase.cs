using UnityEngine;
using System.Collections;
using MidiaAdapt.Core.Interfaces;
using MidiaAdapt.Core.Managers;

namespace MidiaAdapt.Weapon
{
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {        
        public int scoreMaxAmmo { get; private set;  }

        [SerializeField] protected GameObject _bulletPrefab;
        [SerializeField] protected Transform _firePoint;

        [Header("Configuração da arma")]
        public int _currentAmmo;
        [SerializeField] protected int _ammoCapacity;

        [SerializeField] protected float _range;
        [SerializeField] protected float _fireRate;
        [SerializeField] protected float _reloadTime;
        public bool isReloading;

        protected virtual void Start()
        {
            SetMaxAmmo(_ammoCapacity); //Inicia a arma com o carregador cheio
            AddAmmo(_ammoCapacity);
        }

        public abstract void Shoot();

        public virtual void Reload()
        {
            if (isReloading || _currentAmmo == _ammoCapacity) //Impede o jogador de recarregar se já estiver recarregando ou se o carregador estiver cheio
                return;
            StartCoroutine(ReloadCourotine());
        }

        IEnumerator ReloadCourotine()
        {
            isReloading = true;

            PlayerWeaponManager.Instance.OnBecomeReloading();

            yield return new WaitForSeconds(_reloadTime);

            AddAmmo(_ammoCapacity); //Recarrega a arma para o máximo de munição

            isReloading = false;

            PlayerWeaponManager.Instance.OnFinishedReloading();
        }
        
        public void SetMaxAmmo(int amount)
        {
            _ammoCapacity = amount;
            scoreMaxAmmo = _ammoCapacity;
            AddAmmo(_ammoCapacity); //Inicia a arma com o carregador cheio
        }

        public void AddAmmo(int amount)
        {
            _currentAmmo += amount;
            if(_currentAmmo > _ammoCapacity)
            {
                _currentAmmo = _ammoCapacity;
            }
            PlayerWeaponManager.Instance.OnAmmoChanged(_currentAmmo);
        }

        public void RemoveAmmo(int amount)
        {
            _currentAmmo -= amount;
            if(_currentAmmo < 0)
            {
                _currentAmmo = 0;
            }
            PlayerWeaponManager.Instance.OnAmmoChanged(_currentAmmo);
        }
    }
}
