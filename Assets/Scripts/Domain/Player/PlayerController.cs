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
            InputManager.OnShootInputPerformed += HandleShootInput;
            InputManager.OnReloadInputPerformed += HandleReloadInputPerformed;
        }

        void OnDisable()
        {
            InputManager.OnShootInputPerformed -= HandleShootInput;
            InputManager.OnReloadInputPerformed -= HandleReloadInputPerformed;
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
        }

        void HandleShootInput(bool shooting)
        {
            if(_equippedWeapon != null && shooting)
            {
                _equippedWeapon.Shoot();
            }
        }

        void HandleReloadInputPerformed()
        {
            _equippedWeapon?.Reload();
        }
    }
}
