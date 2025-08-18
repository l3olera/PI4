using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MidiaAdapt.Core
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public static event Action<bool> OnJumpInput;

        public InputActionAsset inputActions;

        // Input actions for player controls
        InputAction m_moveAction;
        InputAction m_lookAction;
        InputAction m_jumpAction;

        public Vector2 MoveAmt { get; private set; } //Movement input amount
        public Vector2 LookAmt { get; private set; } //Look input amount

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
            if(Instance == null)
            {
                DontDestroyOnLoad(gameObject); // Keep this instance across scenes
            }
            else
            {
                Destroy(gameObject); // Ensure only one instance exists
            }

            Instance = this; // Set the singleton instance

            m_moveAction = inputActions.FindAction("Move");
            m_lookAction = inputActions.FindAction("Look");
            m_jumpAction = inputActions.FindAction("Jump");
        }

        void Update()
        {
            MoveAmt = m_moveAction.ReadValue<Vector2>();
            LookAmt = m_lookAction.ReadValue<Vector2>();

            if (m_jumpAction.WasPressedThisFrame())
            {
                OnJumpInput?.Invoke(true);
            }

            if (m_jumpAction.WasReleasedThisFrame())
            {
                OnJumpInput?.Invoke(false);
            }
        }
    }
}
