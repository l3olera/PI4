using Player47.Core.Managers;
using Player47.Core.Service;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace Player47.UI.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Panel References")]
        [SerializeField] PanelSwitcher _switcher;
        [SerializeField]
        GameObject[] _panels;

        [SerializeField] GameObject _settingsMenu;
        [SerializeField] GameObject _creditsMenu;
        [SerializeField] GameObject _networkMenu;
        [SerializeField] GameObject _quitMenu;

        [SerializeField] string nextSceneName;

        /*
        [Header("Cutscene Video")]
        [SerializeField] CanvasGroup m_videoCanvas;
        [SerializeField] VideoPlayer m_cutsceneVideo;
        */
        void Start()
        {
            CursorController.UnlockCursor();

            if (_settingsMenu != null && _settingsMenu.activeSelf)
            {
                _settingsMenu.SetActive(false);
            }

            if (_creditsMenu != null && _creditsMenu.activeSelf)
            {
                _creditsMenu.SetActive(false);
            }

            if (_networkMenu != null && _networkMenu.activeSelf)
            {
                _networkMenu.SetActive(false);
            }

            if (_quitMenu != null && _quitMenu.activeSelf)
            {
                _quitMenu.SetActive(false);
            }

            /*
            if (m_videoCanvas.alpha > 0)
            {
                m_videoCanvas.alpha = 0;
            }
            */
        }

        public void StartGame()
        {
            // Inicia direto o jogo sem o vídeo
            FadeManager.Instance.FadeToBlack();
            SceneLoader.Instance.LoadSceneAsync(nextSceneName);
            /*
            m_videoCanvas.alpha = 1;
            m_cutsceneVideo.Play();

            StartCoroutine(WaitVideoEnd());
            */
        }

        /*
        IEnumerator WaitVideoEnd()
        {
            yield return new WaitUntil(() => m_cutsceneVideo.isPrepared); //Espera o video estar preparado 
            yield return new WaitUntil(() => m_cutsceneVideo.isPlaying); //Espera o vídeo tocar
            CursorController.LockCursor();
            yield return new WaitUntil(() => !m_cutsceneVideo.isPlaying); //Ai sim, espera o vídeo terminar  
         
            FadeManager.Instance.FadeToBlack();
            SceneLoader.Instance.LoadSceneAsync(nextSceneName);
        }
        */

        public void ToggleSettings()
        {
            TogglePanel(_settingsMenu);
        }

        public void ToggleCredits()
        {
            TogglePanel(_creditsMenu);
        }

        public void ToggleNetworkMenu()
        {
            TogglePanel(_networkMenu);
        }

        public void ToggleQuitMenu()
        {
            TogglePanel(_quitMenu);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        void TogglePanel(GameObject obj)
        {
            if (obj == null) return;

            _switcher.TogglePanel(obj, _panels);
        }

    }

}