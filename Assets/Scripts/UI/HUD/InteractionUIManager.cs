using UnityEngine;
using TMPro;
using Player47.Core.Service;

namespace Player47.UI.HUD
{
    public class InteractionUIManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_interactionUI;

        LocalizationManager m_localization;

        void Start()
        {
            m_localization = LocalizationManager.Instance;

            HideInteractionText();
        }

        public void ShowInteractionText(string textKey)
        {
            Debug.Log($"Mostrando texto de interação: {textKey}");
            m_interactionUI.SetText(m_localization.GetTranslation(textKey));
        }

        public void HideInteractionText()
        {
            m_interactionUI.SetText(string.Empty);
        }
    }
}