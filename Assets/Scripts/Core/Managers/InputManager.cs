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
        public static event Action InteractInputPerformed;

        public static bool IsShooting;
        public static bool IsAiming;

        public InputActionAsset inputActions;

        InputAction _shootAction;
        InputAction _aimAction;

        Action<InputAction.CallbackContext> _aimStartedDelegate;
        Action<InputAction.CallbackContext> _aimCanceledDelegate;
        Action<InputAction.CallbackContext> _shootPerformedDelegate;
        Action<InputAction.CallbackContext> _shootCanceledDelegate;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        void OnEnable()
        {
            // Ativa os mapas de ação
            inputActions.FindActionMap("Player").Enable();
            inputActions.FindActionMap("ToolsMap").Enable();
            inputActions.FindActionMap("UI").Enable();

            // Pega as ações específicas de atirar e mirar
            _shootAction = inputActions.FindAction("Shoot");
            _aimAction = inputActions.FindAction("Aim");

            // Assina eventos de mira
            _aimStartedDelegate = ctx => OnAim(true);
            _aimCanceledDelegate = ctx => OnAim(false);
            _aimAction.started += _aimStartedDelegate;
            _aimAction.canceled += _aimCanceledDelegate;

            // Assina eventos de tiro
            _shootPerformedDelegate = ctx => OnShoot(true);
            _shootCanceledDelegate = ctx => OnShoot(false);
            _shootAction.performed += _shootPerformedDelegate;
            _shootAction.canceled += _shootCanceledDelegate;

            // Assina os outros eventos
            inputActions.FindAction("Reload").performed += OnReload;
            inputActions.FindAction("UseTorch").performed += OnUseTorch;
            inputActions.FindAction("Pause").performed += OnPause;
            inputActions.FindAction("Interact").performed += OnInteract;
        }

        void OnDisable()
        {
            // Desativa mapas de ação
            inputActions.FindActionMap("Player").Disable();
            inputActions.FindActionMap("ToolsMap").Disable();
            inputActions.FindActionMap("UI").Disable();

            // Remove eventos de mira
            _aimAction.started -= _aimStartedDelegate;
            _aimAction.canceled -= _aimCanceledDelegate;

            // Remove eventos de tiro
            _shootAction.performed -= _shootPerformedDelegate;
            _shootAction.canceled -= _shootCanceledDelegate;

            // Remove outros eventos
            inputActions.FindAction("Reload").performed -= OnReload;
            inputActions.FindAction("UseTorch").performed -= OnUseTorch;
            inputActions.FindAction("Pause").performed -= OnPause;
            inputActions.FindAction("Interact").performed -= OnInteract;
        }

        void OnShoot(bool value)
        {
            if (PauseManager.IsGamePaused.Value) return;

            IsShooting = value;
            ShootInputPerformed?.Invoke(value);
            if (value) Debug.Log("Atirou");
        }

        void OnAim(bool value)
        {
            if (PauseManager.IsGamePaused.Value || IsAiming == value) return;

            IsAiming = value;
            AimInputPerformed?.Invoke(value);
        }

        void OnReload(InputAction.CallbackContext ctx)
        {
            if (PauseManager.IsGamePaused.Value) return;
            ReloadInputPerformed?.Invoke();
        }

        void OnUseTorch(InputAction.CallbackContext ctx)
        {
            if (PauseManager.IsGamePaused.Value) return;
            TorchInputPerformed?.Invoke();
        }

        void OnPause(InputAction.CallbackContext ctx)
        {
            PauseInputPerformed?.Invoke();
        }

        void OnInteract(InputAction.CallbackContext ctx)
        {
            if (PauseManager.IsGamePaused.Value) return;
            InteractInputPerformed?.Invoke();
        }
    }
}
