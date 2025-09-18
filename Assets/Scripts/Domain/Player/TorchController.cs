using Player47.Core.Managers;
using UnityEngine;

namespace Player47.Domain.Player
{
    public class TorchController : MonoBehaviour
    {
        [SerializeField] GameObject _torchLight;

        void OnEnable()
        {
            InputManager.OnTorchInputPerformed += InputManager_OnTorchInputPerformed;
        }

        void OnDisable()
        {
            InputManager.OnTorchInputPerformed -= InputManager_OnTorchInputPerformed;
        }

        void InputManager_OnTorchInputPerformed()
        {
            ToggleTorch();
        }

        public void ToggleTorch()
        {
            Debug.Log("Entrou no toggle");
            if (_torchLight != null)
            {
                _torchLight.SetActive(!_torchLight.activeSelf);
            }
        }
    }
}