using UnityEngine;

namespace MidiaAdapt.UI.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Panel References")]
        [SerializeField] GameObject _settingsMenu;
        [SerializeField] GameObject _creditsMenu;
        [SerializeField] GameObject _networkMenu;
        [SerializeField] GameObject _quitMenu;

        [SerializeField] string nextSceneName;

        void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void StartGame()
        {
            SceneLoader.Instance.LoadScene(nextSceneName);
        }

        public void ToggleSettings(bool active)
        {
            ToggleObject(_settingsMenu, active);
            ToggleObject(_creditsMenu, false);
            ToggleObject(_networkMenu, false);
            ToggleObject(_quitMenu, false);
        }

        public void ToggleCredits(bool active)
        {
            ToggleObject(_creditsMenu, active);
            ToggleObject(_settingsMenu, false);
            ToggleObject(_networkMenu, false);
            ToggleObject(_quitMenu, false);
        }

        public void ToggleNetworkMenu()
        {
            ToggleObject(_networkMenu, !_networkMenu.activeSelf);
            ToggleObject(_settingsMenu, false);
            ToggleObject(_creditsMenu, false);
            ToggleObject(_quitMenu, false);
        }

        public void ToggleQuitMenu()
        {
            ToggleObject(_quitMenu, !_quitMenu.activeSelf);
            ToggleObject(_settingsMenu, false);
            ToggleObject(_creditsMenu, false);
            ToggleObject(_networkMenu, false);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        void ToggleObject(GameObject obj, bool status)
        {
            obj.SetActive(status);
        }
    }

}