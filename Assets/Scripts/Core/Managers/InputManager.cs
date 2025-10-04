using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player47.Core.Managers
{
    /// <summary>
    /// Responsável por gerenciar as entradas do jogador.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public static event Action<bool> ShootInputPerformed;
        public static event Action<bool> AimInputPerformed;
        public static event Action ReloadInputPerformed;
        public static event Action TorchInputPerformed;
        public static event Action PauseInputPerformed;

        public static bool IsShooting;
        public static bool IsAiming;

        public InputActionAsset inputActions;

        InputAction _shootAction;
        InputAction _aimAction;

        void OnEnable()
        {
           inputActions.FindActionMap("Player").Enable();
           inputActions.FindActionMap("ToolsMap").Enable();
           inputActions.FindActionMap("UI").Enable();

           _shootAction = inputActions.FindAction("Shoot");
           _aimAction = inputActions.FindAction("Aim");


            _aimAction.started += ctx => OnAim(true);
            _aimAction.canceled += ctx => OnAim(false);

            inputActions.FindAction("Reload").performed += OnReload;
           inputActions.FindAction("UseTorch").performed += OnUseTorch;
           inputActions.FindAction("Pause").performed += OnPause;
           
        }

        void OnDisable()
        {
            inputActions.FindActionMap("Player").Disable();
            inputActions.FindActionMap("ToolsMap").Disable();
            inputActions.FindActionMap("UI").Disable();

            _aimAction.started -= ctx => OnAim(true);
            _aimAction.canceled -= ctx => OnAim(false);

            inputActions.FindAction("Reload").performed -= OnReload;
            inputActions.FindAction("UseTorch").performed -= OnUseTorch;
            inputActions.FindAction("Pause").performed -= OnPause;
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
            float shootValue = _shootAction.ReadValue<float>();
            float aimValue = _aimAction.ReadValue<float>();


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
            if (PauseManager.IsGamePaused.Value) return;

            Debug.Log("Atirou");
            IsShooting = value;
            ShootInputPerformed?.Invoke(value);
        }

        void OnAim(bool value)
        {
            if(PauseManager.IsGamePaused.Value || IsAiming == value) return;

            IsAiming = value;
            AimInputPerformed?.Invoke(value);
        } 

        void OnReload(InputAction.CallbackContext ctx)
        {
            ReloadInputPerformed?.Invoke();
        }

        void OnUseTorch(InputAction.CallbackContext ctx)
        {
            TorchInputPerformed?.Invoke();
        }

        void OnPause(InputAction.CallbackContext ctx)
        {
            PauseInputPerformed?.Invoke();
        }
    }
}
