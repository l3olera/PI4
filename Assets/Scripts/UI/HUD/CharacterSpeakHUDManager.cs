using TMPro;
using UnityEngine;

namespace Player47.UI.HUD
{
    public class CharacterSpeakHUDManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_textTutorial;

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
            ClearTutorialText();
        }

        public void SetTutorialText(string textKey)
        {
            m_textSetter.SetTranslateText(m_textTutorial, textKey);
        }

        public void ClearTutorialText()
        {
            m_textTutorial.SetText(string.Empty);
        }
    }
}