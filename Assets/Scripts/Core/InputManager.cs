using UnityEngine;
using UnityEngine.InputSystem;

namespace MidiaAdapt.Core
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        public bool isMoving { get; private set; }

        [SerializeField] InputAction _moveAction;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
