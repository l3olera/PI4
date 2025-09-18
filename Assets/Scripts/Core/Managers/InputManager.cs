using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player47.Core.Managers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public static event Action<bool> OnShootInputPerformed;
        public static event Action OnReloadInputPerformed;
        public static event Action OnTorchInputPerformed;

        public static bool IsShooting;

        public InputActionAsset inputActions;

        void OnEnable()
        {
           inputActions.FindActionMap("Player").Enable();
           inputActions.FindActionMap("ToolsMap").Enable();

            inputActions.FindAction("Reload").performed += OnReload;
           inputActions.FindAction("UseTorch").performed += OnUseTorch;
        }

        void OnDisable()
        {
            inputActions.FindActionMap("Player").Disable();
            inputActions.FindActionMap("ToolsMap").Disable();
            inputActions.FindAction("Reload").performed -= OnReload;
            inputActions.FindAction("UseTorch").performed -= OnUseTorch;
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

        void OnReload(InputAction.CallbackContext ctx)
        {
            OnReloadInputPerformed?.Invoke();
        }

        void OnUseTorch(InputAction.CallbackContext ctx)
        {
            Debug.Log("Ele usou a tocha");
            OnTorchInputPerformed?.Invoke();
        }
    }
}
