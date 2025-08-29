using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using MidiaAdapt.Core.Interfaces;
using MidiaAdapt.Core.Managers;
using MidiaAdapt.Weapon.Projectiles;

namespace MidiaAdapt.Weapon
{
    public class Weapon : MonoBehaviour, IWeapon
    {        
        public int scoreMaxAmmo { get; private set;  }
        public bool isReloading;

        [Header("Componentes da arma")]
        [SerializeField] Projectile _projectilePrefab;
        [SerializeField] Transform _firePoint;

        [Header("Configuração da arma")]
        public int _currentAmmo;
        [SerializeField] int _ammoCapacity;

        [Tooltip("Quantitade de bala por segundo que a arma pode disparar")]
        [SerializeField] float _fireRate;
        [SerializeField] float _reloadTime;
        [SerializeField] float _projectileVelocity;

        float _nextTimeToFire = 0f;

        IObjectPool<Projectile> _projectilePool;

        [Header("Pool de balas")]
        [SerializeField] bool _collectionCheck = true; //Joga um erro se tentar devolver um objeto já na pool

        [SerializeField] int defaultCapacity = 10;
        [SerializeField] int maxSize = 20;

        void Awake()
        {
            _projectilePool = new ObjectPool<Projectile>(
                createFunc: CreateProjectile,
                actionOnGet: OnGetFromPool,
                actionOnRelease: OnReleaseToPool,
                actionOnDestroy: OnDestroyPooledObject,
                collectionCheck: _collectionCheck,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize);
        }

        void Start()
        {
            SetMaxAmmo(_ammoCapacity); //Inicia a arma com o carregador cheio
            AddAmmo(_ammoCapacity);
        }

        public void Shoot()
        {
            if (_currentAmmo > 0 && !isReloading && _projectilePool != null)
            {
                if (Time.time >= _nextTimeToFire)
                {
                    Projectile projectileObject = GetProjectile();

                    if (projectileObject == null)
                        return;

                    projectileObject.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation); //Define a posição e rotação do projétil para a posição do ponto de disparo

                    projectileObject.Launch(_projectileVelocity);

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
        
        Projectile GetProjectile()
        {
            return _projectilePool.Get();
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

        // Invoca uma instância do projétil e popula o pool das balas
        Projectile CreateProjectile()
        {
            Projectile projectileInstance = Instantiate(_projectilePrefab);
            projectileInstance.BulletPool = _projectilePool;
            return projectileInstance;
        }

        //Função chamada quando um projétil é retirado da pool
        void OnGetFromPool(Projectile pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        //Função chamada quando um projétil é devolvido para a pool
        void OnReleaseToPool(Projectile pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }

        //Função chamada é excedido o tamanho máximo de items na pool. Assim, destruindo o objeto recente criado
        void OnDestroyPooledObject(Projectile pooledObject)
        {
            if (pooledObject == null || pooledObject.gameObject == null) return;

            Destroy(pooledObject.gameObject);
        }
    }
}
