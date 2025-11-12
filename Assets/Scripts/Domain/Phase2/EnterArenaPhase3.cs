using Player47.Core;
using Player47.Core.Managers;
using Player47.Core.Service;
using UnityEngine;

public class EnterArenaPhase3 : MonoBehaviour, IInteractable
{
    [SerializeField] string m_translateKey = "";
    public string TranslateKey => m_translateKey;

    bool m_canShowText = true;
    public bool CanShowText => m_canShowText;

    [SerializeField] GameObject m_panelConfirm;
    bool m_canInteract = true;

    void Start()
    {
        if(m_panelConfirm.activeSelf)
            m_panelConfirm.SetActive(false);
    }

    public void Interact()
    {
        if (!m_canInteract) return;

        m_panelConfirm.SetActive(true);
        m_canShowText = false;
        StopGame();
    }

    public void ClosePanel()
    {
        m_panelConfirm.SetActive(false);
        m_canShowText = true;
        ReturnGame();
    }

    void StopGame()
    {
        PauseManager.CanListenInputPause = false;
        PauseManager.PauseGame();
        CursorController.UnlockCursor();
    }

    void ReturnGame()
    {
        PauseManager.CanListenInputPause = true;
        PauseManager.ResumeGame();
        CursorController.LockCursor();
    }

    public void GoToPhase3()
    {
        //Faz a lógica de ir para a fase 3
        m_canShowText = false;
        m_canInteract = false;
        m_panelConfirm.SetActive(false);
        ReturnGame();
    }
}
