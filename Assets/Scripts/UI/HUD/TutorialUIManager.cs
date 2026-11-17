using UnityEngine;
using Player47.Core.Managers;
using Player47.Core;
using System;
using System.Collections;

namespace Player47.UI.HUD
{
    public class TutorialUIManager : MonoBehaviour, IEventSource
    {
        [SerializeField] GameObject m_tutorialCanvas;

        public event Action Triggered;

        void Start()
        {
            InputManager.PauseInputPerformed -= ExitTutorialHUD;
            InputManager.PauseInputPerformed += ExitTutorialHUD;

            Triggered?.Invoke();
            if (m_tutorialCanvas != null)
                m_tutorialCanvas.SetActive(true);
            PauseManager.CanListenInputPause = false;

            if (InputManager.CanListenGameplayInputs.Value)
                InputManager.CanListenGameplayInputs.Value = false;
        }

        void OnDisable()
        {
            InputManager.PauseInputPerformed -= ExitTutorialHUD;
        }

        void OnDestroy()
        {
            InputManager.PauseInputPerformed -= ExitTutorialHUD;
        }

        void ExitTutorialHUD()
        {
            InputManager.PauseInputPerformed -= ExitTutorialHUD;

            if (this == null || m_tutorialCanvas == null) return;

            Debug.Log("Entrou no método que tira o tutorial do HUD");
            m_tutorialCanvas.SetActive(false);

            InputManager.CanListenGameplayInputs.Value = true;
            StartCoroutine(EnablePauseNextFrame());
        }

        IEnumerator EnablePauseNextFrame()
        {
            yield return null;
            PauseManager.CanListenInputPause = true;
        }
    }
}