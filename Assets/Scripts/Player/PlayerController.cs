using UnityEngine;
using MidiaAdapt.Core.Managers;
using MidiaAdapt.Core.Interfaces;

namespace MidiaAdapt.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [SerializeField] MonoBehaviour _startinWeapon; //Permite setar a arma inicial pela interface no inspetor

        IWeapon _equippedWeapon;

        void OnEnable()
        {
            InputManager.OnShootInputPerformed += HandleShootInput;
        }

        void OnDisable()
        {
            InputManager.OnShootInputPerformed -= HandleShootInput;
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
        }

        void HandleShootInput(bool shooting)
        {
            Debug.Log("Shoot input received: " + shooting);
            Debug.Log("Equipped weapon: " + (_equippedWeapon));
            if(_equippedWeapon != null && shooting)
            {
                _equippedWeapon.Shoot();
            }
        }
    }
}
