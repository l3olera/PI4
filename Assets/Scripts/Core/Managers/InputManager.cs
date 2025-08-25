using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MidiaAdapt.Core.Managers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public static event Action<bool> OnShootInputPerformed;
        public static event Action<bool> OnRechargeInputPerformed;

        public static bool IsShooting;

        public InputActionAsset inputActions;

        void OnEnable()
        {
           inputActions.FindActionMap("Player").Enable();
        }

        void OnDisable()
        {
            inputActions.FindActionMap("Player").Disable();
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

        void Update()
        {
            float shootValue = inputActions.FindAction("Shoot").ReadValue<float>();


            if (shootValue > 0.5f) // só dispara se o gatilho passar de 50%
            {
                OnShoot(true);
            }
            else
            {
                OnShoot(false);
            }
        }

        void OnShoot(bool value)
        {
            IsShooting = value;
            OnShootInputPerformed?.Invoke(value);
        }

        void Recharge()
        {

        }
    }
}
