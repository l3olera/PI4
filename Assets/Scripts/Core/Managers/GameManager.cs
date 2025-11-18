using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player47.Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }

        void Start()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;

            if (FadeManager.Instance != null)
            {
                FadeManager.Instance.FadeFromBlack();
            }
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (FadeManager.Instance != null)
            {
                FadeManager.Instance.FadeFromBlack();
            }
        }
    }
}
