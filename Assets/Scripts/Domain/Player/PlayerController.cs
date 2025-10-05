using UnityEngine;
using Player47.Core.Managers;
using Player47.Domain.Weapon.Abstractions;

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
        }

        public void EquipWeapon(IWeapon weapon)
        {
            _equippedWeapon = weapon;
            PlayerWeaponHandler.Instance.playerCurrentEquippedWeapon = _equippedWeapon;
            Debug.Log("Arma equipada: " + weapon);
            Debug.Log("Arma equipada: " + _equippedWeapon);
        }

        void InputManager_OnShootInputPerformed(bool shooting)
        {
            Debug.Log("Ele entra no input manager para atirar");
            Debug.Log(_equippedWeapon is null);
            Debug.Log("Ele está atirando? " + shooting);
            if (_equippedWeapon != null && shooting)
            {
                Debug.Log("Ele entra na arma equipada para atirar");
                _equippedWeapon.Shoot();
            }
        }

        void InputManager_OnReloadInputPerformed()
        {
            _equippedWeapon?.Reload();
        }
    }
}
