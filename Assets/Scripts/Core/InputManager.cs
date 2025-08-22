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

            m_moveAction = inputActions.FindAction("Move"); //Setup move input action
            m_lookAction = inputActions.FindAction("Look"); //Setup look input action
            m_jumpAction = inputActions.FindAction("Jump"); //Setup jump input action
        }

        void Update()
        {
            MoveAmt = m_moveAction.ReadValue<Vector2>(); //Read movement input value
            LookAmt = m_lookAction.ReadValue<Vector2>(); //Read look input value

            if (m_jumpAction.WasPressedThisFrame()) //Check if jump input was pressed this frame
            {
                TriggerJumpInput(true);
            }

            if (m_jumpAction.WasReleasedThisFrame())
            {
                TriggerJumpInput(false);
            }
        }

        void TriggerJumpInput(bool isJumping)
        {
            OnJumpInput?.Invoke(isJumping);
        }
    }
}
