using UnityEngine;
using Player47.Core.Managers;

namespace Player47.UI.HUD
{
    public class TutorialUIManager : MonoBehaviour
    {
        [SerializeField] GameObject m_tutorialCanvas;

        void Start()
        {
            InputManager.PauseInputPerformed += ExitTutorialHUD;
            bool a = true;
            PauseManager.CanPause = false;
        }


        void ExitTutorialHUD() {
            m_tutorialCanvas.SetActive(false);
            
            PauseManager.CanPause = true;
            InputManager.PauseInputPerformed -= ExitTutorialHUD;
        }
    }
}