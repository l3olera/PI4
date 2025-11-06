using UnityEngine;
using Player47.Core;
using Player47.Core.Managers;
using Player47.Domain.Totem;

namespace Player47.Domain.Phase1
{
    /// <summary>
    /// Controla a interação do jogador com a entrância para fase 2-1.
    /// </summary>
    public class EntranceTumb : MonoBehaviour, IInteractable
    {
        [SerializeField] string m_translateKeyUI;
        [SerializeField] bool m_canShowText = true;

        [SerializeField] bool m_canInteract = false;

        public string TranslateKey => m_translateKeyUI;

        public bool CanShowText => m_canShowText;

        void OnEnable()
        {
            if (TotemManager.Instance == null)
                Debug.LogError("TotemManager ainda não foi inicializado!");
            else
                Debug.Log("EntranceTumb inscrita no evento!");

            TotemManager.Instance.AllTotemsDeactivated.AddListener(OnAllTotemsDeactivated);
        }

        void OnDisable()
        {
        TotemManager.Instance.AllTotemsDeactivated.RemoveListener(OnAllTotemsDeactivated);
        }

        public void Interact()
        {
            if (!m_canInteract) return;

            Phase1Manager.Instance.LoadPhase2_1();
        }

        void OnAllTotemsDeactivated()
        {
            Debug.Log("Entrada da tumba liberada.");
            m_canInteract = true;
        }
    }
}
