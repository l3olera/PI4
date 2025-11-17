using Codice.Client.Common;
using Player47.Core;
using Player47.Core.Managers;
using Player47.Core.Service;
using System.Collections;
using UnityEngine;

public class EnterArenaPhase3 : MonoBehaviour, IInteractable
{
    [SerializeField] string m_translateKey = "";
    public string TranslateKey => m_translateKey;

    bool m_canShowText = true;
    public bool CanShowText => m_canShowText;

    [SerializeField] GameObject m_panelConfirm;
    bool m_canInteract = true;

    [Header("Phase 3 Doors")]
    [SerializeField] Animator[] m_doorsAnim;
    [SerializeField] AudioSource[] m_doorsAudio;

    [Header("Boss Audio")]
    [SerializeField] AudioSource m_bossAudioSource;
    [SerializeField] Animator m_bossAnimator;

    [Header("Next Scene")]
    [SerializeField] string m_nextSceneName = "Credits";

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
        m_canShowText = false;
        m_canInteract = false;
        m_panelConfirm.SetActive(false);
        ReturnGame();

        StartCoroutine(StartBossAudio());

        //Faz a lógica de ir para a fase 3
        for (int i = 0; i < m_doorsAudio.Length; i++)
        {
            m_doorsAudio[i].Play();
            m_doorsAnim[i].SetTrigger("Open");
        }
    }

    IEnumerator StartBossAudio()
    {
        yield return new WaitForSeconds(1f);

        m_bossAudioSource.Play();
        m_bossAnimator.SetTrigger("isAttacking");
        m_bossAnimator.speed = 1.5f;
        StartCoroutine(GoToNextScene());
    }

    IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(3f);
        FadeManager.Instance.FadeToBlack();
        SceneLoader.Instance.LoadSceneAsync(m_nextSceneName);
    }
}
