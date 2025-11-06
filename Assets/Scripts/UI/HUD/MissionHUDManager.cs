using UnityEngine;
using TMPro;

namespace Player47.UI.HUD
{
    public class MissionHUDManager : MonoBehaviour
    {
        public static MissionHUDManager Instance { get; private set; }

        [SerializeField] TextMeshProUGUI m_textMission;

        [SerializeField] TextUITranslateSetter m_translateSetter;

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            if (m_translateSetter == null)
            {
                m_translateSetter = FindFirstObjectByType<TextUITranslateSetter>();
            }
        }

        void Start()
        {
            ClearMissionText();
        }

        public void SetMissionText(string keyTranslateText)
        {
            m_translateSetter.SetTranslateText(m_textMission, keyTranslateText);
        }

        public void ClearMissionText()
        {
            m_textMission.SetText(string.Empty);
        }
    }

}