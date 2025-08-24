using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MidiaAdapt.Core.Managers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public static event Action<bool> OnShootInput;

        public static bool IsShooting;

        public InputActionAsset inputActions;
        void OnEnable()
        {
           inputActions.FindActionMap("Player").Enable();
           inputActions.FindAction("Shoot").performed += Shoot;
        }

        void OnDisable()
        {
            inputActions.FindActionMap("Player").Disable();
            inputActions.FindAction("Shoot").performed -= Shoot;
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

        void Shoot(InputAction.CallbackContext ctx)
        {
            OnShootInput?.Invoke(!IsShooting);
        }
    }
}
