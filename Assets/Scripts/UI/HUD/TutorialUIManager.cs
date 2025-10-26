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

            PauseManager.CanListenInputPause = false;
            PauseManager.PauseGame();
        }


        void ExitTutorialHUD() {
            Debug.Log("Entrou no método que tira o tutorial do HUD");
            m_tutorialCanvas.SetActive(false);

            PauseManager.ResumeGame();
            PauseManager.CanListenInputPause = true;
            InputManager.PauseInputPerformed -= ExitTutorialHUD;
        }
    }
}