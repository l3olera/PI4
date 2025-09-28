using UnityEngine;
using UnityEngine.SceneManagement;
using Player47.Core.Managers;
using Player47.Core.Service;
using Player47.UI;

namespace Player47.SceneManagement
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] GameObject _player;
        [SerializeField] GameObject _pauseManager;

        void OnEnable()
        {
            SceneManager.sceneLoaded += HandleSceneLoaded; 
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= HandleSceneLoaded;    
        }

        void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Menu")
            {
                UIManager.Instance.ShowHUDGameplay(false);
                _player.SetActive(false);
                _pauseManager.SetActive(false);
                CursorController.UnlockCursor();
            }
            else // gameplay
            {
                UIManager.Instance.ShowHUDGameplay(true);
                _player.SetActive(true);
                _pauseManager.SetActive(true);
                CursorController.LockCursor();
            }
        }

    }

}