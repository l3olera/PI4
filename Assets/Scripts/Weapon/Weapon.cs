using System.Collections;
using UnityEngine;
using MidiaAdapt.Core.Interfaces;
using MidiaAdapt.Core.Managers;

namespace MidiaAdapt.Weapon
{
    public class Weapon : MonoBehaviour, IWeapon
    {        
        public int scoreMaxAmmo { get; private set;  }
        public bool isReloading;

        [Header("Componentes da arma")]
        [SerializeField] Camera _playerCamera;

        [Header("Configuração da arma")]
        public int _currentAmmo;
        [SerializeField] int _ammoCapacity;
        [SerializeField] int _damage = 10;
        [Tooltip("Quantitade de bala por segundo que a arma pode disparar")]
        [SerializeField] float _fireRate;
        [SerializeField] float _range;
        [SerializeField] float _reloadTime;
        [SerializeField] LayerMask _hittableLayers;

        float _nextTimeToFire = 0f;

        void Start()
        {
            if(_playerCamera == null)
                _playerCamera = Camera.main;

            SetMaxAmmo(_ammoCapacity); //Inicia a arma com o carregador cheio
            AddAmmo(_ammoCapacity);
        }

        public void Shoot()
        {
            if (_currentAmmo > 0 && !isReloading)
            {
                if (Time.time >= _nextTimeToFire)
                {
                    Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); //Cria um raio a partir do centro da tela

                    if (Physics.Raycast(ray, out RaycastHit hitInfo, _range, _hittableLayers))
                    {
                        if (hitInfo.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                        {
                            damageable.TakeDamage(_damage); //Aplica o dano ao objeto atingido
                        }
                    }

                    RemoveAmmo(1);

                    _nextTimeToFire = Time.time + 1f / _fireRate; //Armazena o tempo para o próximo tiro
                }
            }
        }

        public void Reload()
        {
            if (isReloading || _currentAmmo == _ammoCapacity) //Impede o jogador de recarregar se já estiver recarregando ou se o carregador estiver cheio
                return;
            StartCoroutine(ReloadCourotine());
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
            if (_currentAmmo > _ammoCapacity)
            {
                _currentAmmo = _ammoCapacity;
            }
            PlayerWeaponManager.Instance.OnAmmoChanged(_currentAmmo);
        }

        public void RemoveAmmo(int amount)
        {
            _currentAmmo -= amount;
            if (_currentAmmo < 0)
            {
                _currentAmmo = 0;
            }
            PlayerWeaponManager.Instance.OnAmmoChanged(_currentAmmo);
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
    }
}
