using Player47.Core.Managers;
using Player47.Core.Service;
using UnityEngine;

namespace Player47.UI.Pause
{
    /// <summary>
    /// Responsável por gerenciar a interface de usuário (UI) do menu de pausa. 
    /// 
    /// Desativa o HUD quando o jogo está pausado e ativa o menu de pausa.
    /// Se inscrevendo no evento do <see cref="PauseManager"/> para atualizar a UI conforme o estado de pausa do jogo.
    /// </summary>
    public class PauseUIManager : MonoBehaviour
    {
        [SerializeField] GameObject m_pauseMenuUI;
        [SerializeField] GameObject m_HUDMenuUi;

        void OnEnable()
        {
            Debug.Log("Ativado");
            PauseManager.IsGamePaused.AddListener(PauseManager_IsGamePaused);
            PauseManager_IsGamePaused(PauseManager.IsGamePaused.Value); // já força atualizar com valor atual
        }

        void OnDisable()
        {
            PauseManager.IsGamePaused.RemoveListener(PauseManager_IsGamePaused);
        }

        void PauseManager_IsGamePaused(bool active)
        {
            if (this == null || m_pauseMenuUI == null || m_HUDMenuUi == null) return;

            Debug.Log("Entrou no handle do UI do pause");
            if (!PauseManager.CanListenInputPause) return;

            if (active)
            {
                Debug.Log("Entrando no true");
                CursorController.UnlockCursor();
                m_pauseMenuUI.SetActive(true);
                m_HUDMenuUi.SetActive(false);
            }
            else
            {
                Debug.Log("Entrando no false");
                CursorController.LockCursor();
                m_pauseMenuUI.SetActive(false);
                m_HUDMenuUi.SetActive(true);
            }
        }
    }
}