using Player47.Core;
using Player47.Core.Managers;
using Player47.Core.Service;
using Player47.Domain.Enemy;
using Player47.Domain.Enemy.Types;
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

    [Header("Boss")]
    [SerializeField] AudioSource m_bossAudioSource;
    [SerializeField] Animator m_bossAnimator;
    [SerializeField] OgreEnemy m_bossEnemy;

    [Header("Next Scene")]
    [SerializeField] string m_nextSceneName = "Credits";

    void Start()
    {
        if (m_panelConfirm != null && m_panelConfirm.activeSelf)
            m_panelConfirm.SetActive(false);

        if (m_bossEnemy == null && m_bossAnimator != null)
            m_bossEnemy = m_bossAnimator.GetComponent<OgreEnemy>();

        if (m_bossEnemy != null)
            m_bossEnemy.OnDeath += OnBossDied;
    }

    void OnDestroy()
    {
        if (m_bossEnemy != null)
            m_bossEnemy.OnDeath -= OnBossDied;
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

        if (TryGetComponent<Collider>(out Collider doorCollider))
        {
            doorCollider.enabled = false;
        }

        StartCoroutine(StartBossAudio());

        // Abre as portas para o jogador enfrentar o boss na arena
        for (int i = 0; i < m_doorsAudio.Length; i++)
        {
            m_doorsAudio[i].Play();
            m_doorsAnim[i].SetTrigger("Open");
        }
    }

    IEnumerator StartBossAudio()
    {
        yield return new WaitForSeconds(1f);

        if (m_bossAudioSource != null)
            m_bossAudioSource.Play();

        if (m_bossEnemy != null)
        {
            m_bossEnemy.ActivateBoss();
        }
    }

    void OnBossDied(Enemy boss)
    {
        StartCoroutine(GoToNextScene());
    }

    IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(3f);
        FadeManager.Instance.FadeToBlack();
        SceneLoader.Instance.LoadSceneAsync(m_nextSceneName);
    }
}