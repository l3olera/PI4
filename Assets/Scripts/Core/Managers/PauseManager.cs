using UnityEngine;
using Player47.Core.Service;

namespace Player47.Core.Managers
{
    /// <summary>
    /// Responsável por gerenciar o estado de pausa do jogo.
    /// 
    /// Se inscreve no evento do <see cref="InputManager"/> para detectar quando o jogador solicita pausar ou retomar o jogo.
    /// </summary>
    public class PauseManager : MonoBehaviour
    {
        public static Observer<bool> IsGamePaused { get; } = new(false);

        public static bool CanListenInputPause = true;

        void OnEnable()
        {
            InputManager.PauseInputPerformed += InputManager_OnPauseInputPerformed;
        }

        void OnDisable()
        {
            InputManager.PauseInputPerformed -= InputManager_OnPauseInputPerformed;
        }

        void Start()
        {
            IsGamePaused.Value = false;    
        }

        void InputManager_OnPauseInputPerformed()
        {
            if (!CanListenInputPause) return;

            if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        public static void PauseGame()
        {
            IsGamePaused.Value = true;
            Time.timeScale = 0f;
        }

        public static void ResumeGame()
        {
            IsGamePaused.Value = false;
            Time.timeScale = 1f;
        }
    }
}