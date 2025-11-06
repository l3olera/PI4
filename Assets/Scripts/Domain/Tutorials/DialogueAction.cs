using Player47.Core;
using Player47.UI.HUD;
using System.Collections;
using UnityEngine;

namespace Player47.Domain.Tutorials
{
    public class DialogueAction : MonoBehaviour, IAction
    {
        public string keyText;
        public float durationText = 3f;

        [SerializeField] CharacterSpeakHUDManager m_speakHUDManager;

        void Start()
        {
            if(m_speakHUDManager == null)
            {
                m_speakHUDManager = FindAnyObjectByType<CharacterSpeakHUDManager>();
            }
        }

        public void Execute()
        {
            m_speakHUDManager.SetTutorialText(keyText);
            StartCoroutine(HideText());
        }

        IEnumerator HideText()
        {
            yield return new WaitForSeconds(durationText);
            m_speakHUDManager.ClearTutorialText();
        }
    }
}