using UnityEngine;
using Player47.Core.Managers;
using Player47.Core;
using System;

namespace Player47.UI.HUD
{
    public class TutorialUIManager : MonoBehaviour, ITutorialTrigger
    {
        [SerializeField] GameObject m_tutorialCanvas;

        public event Action Triggered;

        void Start()
        {
            InputManager.PauseInputPerformed += ExitTutorialHUD;

            Triggered?.Invoke();
            m_tutorialCanvas.SetActive(true);
            PauseManager.CanListenInputPause = false;
            InputManager.CanListenGameplayInputs.Value = false;
        }


        void ExitTutorialHUD() {
            Debug.Log("Entrou no método que tira o tutorial do HUD");
            m_tutorialCanvas.SetActive(false);

            PauseManager.CanListenInputPause = true;
            InputManager.CanListenGameplayInputs.Value = true;
            InputManager.PauseInputPerformed -= ExitTutorialHUD;
        }
    }
}