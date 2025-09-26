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
            IsGamePaused.Value = true;
            CursorController.UnlockCursor();
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            IsGamePaused.Value = false;
            CursorController.LockCursor();
            Time.timeScale = 1f;
        }
    }
}