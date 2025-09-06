using UnityEngine;
using Player47.Core.Managers;
using Player47.Core.Service;

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

        void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if(_settingsMenu != null && _settingsMenu.activeSelf)
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
        }

        public void StartGame()
        {
            SceneLoader.Instance.LoadScene(nextSceneName);
        }

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