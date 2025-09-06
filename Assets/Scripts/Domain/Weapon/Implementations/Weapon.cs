using System.Collections;
using UnityEngine;
using Player47.Domain.Combat;
using Player47.Domain.Weapon.Abstractions;
using Player47.Domain.Player;

namespace Player47.Domain.Weapon.Implementations
{
    /// <summary>
    /// Arma que pode atirar, recarregar e causar dano a objetos que implementam a interface IDamageable.
    /// </summary>
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
                    DetectHit(); //Detecta se o tiro atingiu algo e aplica o dano

                    RemoveAmmo(1); //Remove uma bala do carregador

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

        /// <summary>
        /// Define o máximo e a capacidade de munição do carregador.
        /// </summary>
        /// <param name="amount">Quantidade a ser definido o máximo da capacidade</param>
        public void SetMaxAmmo(int amount)
        {
            _ammoCapacity = amount;
            scoreMaxAmmo = _ammoCapacity;
            AddAmmo(_ammoCapacity); //Inicia a arma com o carregador cheio
        }

        /// <summary>
        /// Adiciona uma quantidade específica de munição do carregador.
        /// </summary>
        /// <param name="amount">Quantidade de munição a ser adcionada</param>
        public void AddAmmo(int amount)
        {
            _currentAmmo += amount;
            if (_currentAmmo > _ammoCapacity)
            {
                _currentAmmo = _ammoCapacity;
            }
            PlayerWeaponHandler.Instance.OnAmmoChanged(_currentAmmo);
        }

        /// <summary>
        /// Remove uma quantidade específica de munição do carregador.
        /// </summary>
        /// <param name="amount">Quantidade de munição a ser tirada</param>
        public void RemoveAmmo(int amount)
        {
            _currentAmmo -= amount;
            if (_currentAmmo < 0)
            {
                _currentAmmo = 0;
            }
            PlayerWeaponHandler.Instance.OnAmmoChanged(_currentAmmo);
        }
 
        IEnumerator ReloadCourotine()
        {
            isReloading = true;

            PlayerWeaponHandler.Instance.OnBecomeReloading();

            yield return new WaitForSeconds(_reloadTime);

            AddAmmo(_ammoCapacity); //Recarrega a arma para o máximo de munição

            isReloading = false;

            PlayerWeaponHandler.Instance.OnFinishedReloading();
        }

        /// <summary>
        /// Detecta se o tiro atingiu um objeto que pode receber dano e aplica o dano.
        /// </summary>
        void DetectHit()
        {
            Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); //Cria um raio a partir do centro da tela
#if UNITY_EDITOR
            Debug.DrawRay(ray.origin, ray.direction * _range, Color.red, 1f); //Desenha o raio para fins de depuração
#endif
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _range, _hittableLayers))
            {
                if (hitInfo.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.TakeDamage(_damage); //Aplica o dano ao objeto atingido
                }
            }
        }
    }
}
