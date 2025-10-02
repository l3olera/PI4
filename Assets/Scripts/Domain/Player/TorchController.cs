using Player47.Core.Managers;
using Player47.Core.Service;
using UnityEngine;

namespace Player47.Domain.Player
{
    /// <summary>
    /// Responsável por controlar o estado da tocha do jogador.
    /// </summary>
    public class TorchController : MonoBehaviour
    {
        /// <summary>
        /// Identifica se o player está com a tocha equipada e dispara evento quando o valor mudar por ser uma classe <see cref="Observer{T}"/>.
        /// </summary>

        public static Observer<bool> IsTorchEquipped { get; } = new(false);

        public static bool TorchEquippedValue => IsTorchEquipped.Value;


        void OnEnable()
        {
            InputManager.TorchInputPerformed += InputManager_OnTorchInputPerformed;
        }

        void OnDisable()
        {
            InputManager.TorchInputPerformed -= InputManager_OnTorchInputPerformed;
        }

        void InputManager_OnTorchInputPerformed()
        {
            ToggleTorch();
        }

        public void ToggleTorch()
        {
            IsTorchEquipped.Value = !IsTorchEquipped.Value;
        }
    }
}