using UnityEngine;

namespace Player47.Domain.Player
{
    /// <summary>
    /// Controla todos os aspectos de animação do jogador.
    /// </summary>
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] Animator _anim;

        void Awake()
        {
            if(gameObject.TryGetComponent<Animator>(out var animator))
            {
                _anim = animator;
            }
        }

        void OnEnable()
        {
            TorchController.IsTorchEquipped.AddListener(TorchController_IsTorchEquipped_OnValueChanged);
        }

        void OnDisable()
        {
            TorchController.IsTorchEquipped.RemoveListener(TorchController_IsTorchEquipped_OnValueChanged);
        }

        void TorchController_IsTorchEquipped_OnValueChanged(bool isEquipped)
        {
            if (isEquipped)
            {
                EquipTorch();
            }
            else
            {
                UnequipTorch();
            }
        }

        void EquipTorch() {             
            _anim.SetTrigger("EquipTorch");
        }

        void UnequipTorch()
        {
            _anim.SetTrigger("UnequipTorch");
        }
    }
}