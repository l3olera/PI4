using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MidiaAdapt.Core
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public static event Action<bool> OnShootInput;

        public InputActionAsset inputActions;

        public static bool isShooting;

        void OnEnable()
        {
           inputActions.FindActionMap("Player").Enable();
           inputActions.FindAction("Shoot").performed += ctx => OnShootInput?.Invoke(!isShooting);
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
    }
}
