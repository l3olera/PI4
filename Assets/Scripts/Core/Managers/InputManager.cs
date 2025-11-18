using Player47.Core.Service;
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

        public static Observer<bool> CanListenGameplayInputs = new(true);

        InputAction _shootAction;
        InputAction _aimAction;

        Action<InputAction.CallbackContext> _aimStartedDelegate;
        Action<InputAction.CallbackContext> _aimCanceledDelegate;

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

        void OnDisable()
        {
            // Desativa mapas de ação
            inputActions.FindActionMap("Player").Disable();
            inputActions.FindActionMap("ToolsMap").Disable();
            inputActions.FindActionMap("UI").Disable();
            inputActions.FindActionMap("Combat").Disable();

            // Remove eventos de mira
            _aimAction.started -= _aimStartedDelegate;
            _aimAction.canceled -= _aimCanceledDelegate;

            // Remove outros eventos
            inputActions.FindAction("Reload").performed -= OnReload;
            inputActions.FindAction("UseTorch").performed -= OnUseTorch;
            inputActions.FindAction("Pause").performed -= OnPause;
            inputActions.FindAction("Interact").performed -= OnInteract;

            PauseManager.IsGamePaused.RemoveListener(PauseManager_IsGamePaused);
        }

        void Start()
        {
            // Ativa os mapas de ação
            inputActions.FindActionMap("Player").Enable();
            inputActions.FindActionMap("ToolsMap").Enable();
            inputActions.FindActionMap("UI").Enable();
            inputActions.FindActionMap("Combat").Enable();

            // Pega as ações específicas de atirar e mirar
            _shootAction = inputActions.FindAction("Shoot");
            _aimAction = inputActions.FindAction("Aim");

            // Assina eventos de mira
            _aimStartedDelegate = ctx => OnAim(true);
            _aimCanceledDelegate = ctx => OnAim(false);
            _aimAction.started += _aimStartedDelegate;
            _aimAction.canceled += _aimCanceledDelegate;

            // Assina os outros eventos
            inputActions.FindAction("Reload").performed += OnReload;
            inputActions.FindAction("UseTorch").performed += OnUseTorch;
            inputActions.FindAction("Pause").performed += OnPause;
            inputActions.FindAction("Interact").performed += OnInteract;

            PauseManager.IsGamePaused.AddListener(PauseManager_IsGamePaused);
        }

        void Update()
        {
            float value = _shootAction.ReadValue<float>();
            
            if(value > 0.5f)
            {
                OnShoot(true);
            }
            else
            {
                OnShoot(false);
            }
        }

        void PauseManager_IsGamePaused(bool isGamePaused)
        {
            CanListenGameplayInputs.Value = !isGamePaused;
        }

        void OnShoot(bool value)
        {
            if (!CanListenGameplayInputs.Value || (!IsShooting && !value)) return;

            IsShooting = value;
            ShootInputPerformed?.Invoke(value);
            if (value) Debug.Log("Atirou");
        }

        void OnAim(bool value)
        {
            if (!CanListenGameplayInputs.Value || IsAiming == value) return;

            IsAiming = value;
            AimInputPerformed?.Invoke(value);
        }

        void OnReload(InputAction.CallbackContext ctx)
        {
            if (!CanListenGameplayInputs.Value) return;
            ReloadInputPerformed?.Invoke();
        }

        void OnUseTorch(InputAction.CallbackContext ctx)
        {
            if (!CanListenGameplayInputs.Value) return;
            TorchInputPerformed?.Invoke();
        }

        void OnPause(InputAction.CallbackContext ctx)
        {
            PauseInputPerformed?.Invoke();
        }

        void OnInteract(InputAction.CallbackContext ctx)
        {
            if (!CanListenGameplayInputs.Value) return;
            InteractInputPerformed?.Invoke();
        }
    }
}
