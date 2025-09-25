using Player47.Core.Managers;
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
            PauseManager.IsGamePaused.AddListener(PauseManager_IsGamePaused);
        }

        void OnDisable()
        {
            PauseManager.IsGamePaused.RemoveListener(PauseManager_IsGamePaused);
        }

        void PauseManager_IsGamePaused(bool active)
        {
            if (active)
            {
                m_pauseMenuUI.SetActive(true);
                m_HUDMenuUi.SetActive(false);
            }
            else
            {
                m_pauseMenuUI.SetActive(false);
                m_HUDMenuUi.SetActive(true);
            }
        }
    }
}
