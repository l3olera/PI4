using TMPro;
using UnityEngine;

namespace Player47.UI.HUD
{
    public class InteractionUIManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_interactionUI;

        [SerializeField] TextUITranslateSetter m_textSetter;

        void Awake()
        {
            if (m_textSetter == null)
            {
                m_textSetter = FindFirstObjectByType<TextUITranslateSetter>();
            }
        }

        void Start()
        {
            HideInteractionText();
        }

        public void ShowInteractionText(string textKey)
        {
            Debug.Log($"Mostrando texto de interação: {textKey}");
            m_textSetter.SetTranslateText(m_interactionUI, textKey);
        }

        public void HideInteractionText()
        {
            m_interactionUI.SetText(string.Empty);
        }
    }
}