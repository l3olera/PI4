using UnityEngine;

namespace Player47.Core.Managers
{
    public class PauseManager : MonoBehaviour
    {
        void OnEnable()
        {
            InputManager.OnPauseInputPerformed += InputManager_OnPauseInputPerformed;
        }

        void OnDisable()
        {
            InputManager.OnPauseInputPerformed -= InputManager_OnPauseInputPerformed;
        }

        void InputManager_OnPauseInputPerformed()
        {
            if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            // Aqui voc� pode adicionar mais l��gica, como mostrar o menu de pausa
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            // Aqui voc� pode adicionar mais l��gica, como esconder o menu de pausa
        }
    }
}