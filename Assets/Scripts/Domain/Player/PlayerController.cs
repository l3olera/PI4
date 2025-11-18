using UnityEngine;
using Player47.Core.Managers;
using Player47.Domain.Weapon.Abstractions;
using Player47.Core;
using UnityEngine.SceneManagement;

namespace Player47.Domain.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [SerializeField] MonoBehaviour _startinWeapon; //Permite setar a arma inicial pela interface no inspetor

        IWeapon _equippedWeapon;

        void OnEnable()
        {
            InputManager.ShootInputPerformed += InputManager_OnShootInputPerformed;
            InputManager.ReloadInputPerformed += InputManager_OnReloadInputPerformed;
        }

        void OnDisable()
        {
            InputManager.ShootInputPerformed -= InputManager_OnShootInputPerformed;
            InputManager.ReloadInputPerformed -= InputManager_OnReloadInputPerformed;
        }

        void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        void Start()
        {
            EquipWeapon(_equippedWeapon ?? (IWeapon)_startinWeapon);
#if UNITY_EDITOR
            if(SpawnPoint.Instance == null)
            {
                Debug.LogWarning("SpawnPoint instance is null. Make sure there is a SpawnPoint in the scene.");
                return;
            }
            SpawnPoint.Instance.RespawnPlayerAtPosition(transform);
#endif
        }


        public void EquipWeapon(IWeapon weapon)
        {
            _equippedWeapon = weapon;
            PlayerWeaponHandler.Instance.playerCurrentEquippedWeapon = _equippedWeapon;
            PlayerWeaponHandler.Instance.OnAmmoChanged(_equippedWeapon.ScoreMaxAmmo);
        }

        void InputManager_OnShootInputPerformed(bool shooting)
        {
            Debug.Log(_equippedWeapon is null);
            if (_equippedWeapon != null && shooting)
            {
                _equippedWeapon.Shoot();
            }
        }

        void InputManager_OnReloadInputPerformed()
        {
            _equippedWeapon?.Reload();
        }
    }
}
