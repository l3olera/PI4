using Player47.Core.Service;
using TMPro;
using UnityEngine;

namespace Player47.UI.HUD
{
    public class TextUITranslateSetter : MonoBehaviour
    {
        LocalizationManager m_localization;

        void Start()
        {
            m_localization = LocalizationManager.Instance;
        }

        public void SetTranslateText(TextMeshProUGUI textUI, string key)
        {
            if(m_localization == null) m_localization = LocalizationManager.Instance;

            Debug.Log($"<color=green>A chave {key} trouxe a seguinte tradução: {m_localization.GetTranslation(key)}</color>");

            textUI.SetText(m_localization.GetTranslation(key));
        }
    }
}