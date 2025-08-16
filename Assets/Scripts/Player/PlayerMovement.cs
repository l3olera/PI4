using UnityEngine;
using UnityEngine.InputSystem;

namespace MidiaAdapt.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] InputAction _moveAction;

        [SerializeField] float _speedMovement = 5f;

        CharacterController _cc;

        private void OnEnable()
        {
            _moveAction.Enable();
        }

        void Awake()
        {
           _cc = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
        }
    }
}
