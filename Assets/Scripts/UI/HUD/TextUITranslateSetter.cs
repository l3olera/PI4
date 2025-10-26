using Player47.Core.Service;
using TMPro;
using UnityEngine;

namespace Player47.UI.HUD
{
    public class TextUITranslateSetter : MonoBehaviour
    {
        LocalizationManager m_localization;

        TextMeshProUGUI m_lastTextUI;
        string m_lastText;

        void Start()
        {
            m_localization = LocalizationManager.Instance;
            LocalizationManager.OnFinishTranslations += OnFinishTranslations;
        }

        void OnDisable()
        {
            LocalizationManager.OnFinishTranslations -= OnFinishTranslations;
        }

        public void SetTranslateText(TextMeshProUGUI textUI, string key)
        {
            if(m_localization == null) m_localization = LocalizationManager.Instance;

            if(m_lastText != textUI.text)
            {
                m_lastText = textUI.text;
            }

            if(m_lastTextUI != textUI)
            { 
                m_lastTextUI = textUI;
            }

            textUI.SetText(m_localization.GetTranslation(key));
        }

        void OnFinishTranslations() 
        {
            SetTranslateText(m_lastTextUI, m_lastText);
        }
    }
}