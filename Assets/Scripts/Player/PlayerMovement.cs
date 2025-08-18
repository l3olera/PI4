using UnityEngine;
using MidiaAdapt.Core;

namespace MidiaAdapt.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float _speedMovement = 5f;

        CharacterController _cc;

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
