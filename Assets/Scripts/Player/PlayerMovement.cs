using UnityEngine;
using MidiaAdapt.Core;

namespace MidiaAdapt.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Configurações de Movimento")]
        [SerializeField] float _speedMovement = 5f;
        [SerializeField] float _speedRotation = 5f;
        
        [Header("Configurações de Pulo")]
        [SerializeField] float _jumpHeight = 5f;
        [SerializeField] float gravity = -9.81f;

        CharacterController _cc;

        Vector2 _moveInput;
        Vector2 _lookInput;
        bool _isJumping;
        Vector3 _velocity;

        void OnEnable()
        {
            InputManager.OnJumpInput += OnJumpingInput;
        }

        private void OnDisable()
        {
            InputManager.OnJumpInput -= OnJumpingInput;
        }

        void Awake()
        {
           _cc = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _moveInput = InputManager.Instance.MoveAmt;

            Walking();
            Rotating();
            Jumping();
        }

        void Walking()
        {
            Vector3 moveDirection = new(_moveInput.x * _speedMovement, 0, _moveInput.y * _speedMovement);
            _cc.Move(moveDirection * Time.deltaTime);
        }

        void Rotating()
        {
            _lookInput = InputManager.Instance.LookAmt;
            // Rotate the player based on look input
            Vector3 rotation = new(0, _lookInput.x * _speedRotation, 0);
            transform.Rotate(rotation * Time.deltaTime);
        }

        void Jumping()
        {
            // Resetar velocity Y se está no chão
            if (_cc.isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f; // pequeno valor negativo para manter colado no chão
            }

            // Gravidade
            _velocity.y += gravity;

            // Aplica velocidade vertical
            _cc.Move(_velocity * Time.deltaTime);
        }

        void OnJumpingInput(bool isJumping)
        {
            // Pulo
            if (_cc.isGrounded && _isJumping)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * gravity);
            }
        }
    }
}
