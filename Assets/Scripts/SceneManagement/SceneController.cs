using Player47.Core.Service;
using Player47.Domain.Player;
using Player47.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player47.SceneManagement
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] GameObject _player;
        [SerializeField] GameObject _pauseManager;

        bool _wasInSceneGameplay;

        void OnEnable()
        {
            SceneManager.sceneLoaded += HandleSceneLoaded; 
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= HandleSceneLoaded;    
        }

        void Start()
        {
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                UIManager.Instance.ShowHUDGameplay(false);
                _player.SetActive(false);
                _pauseManager.SetActive(false);
                CursorController.UnlockCursor();
                TorchController.CanUseTorch = false;
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "Phase1") return;
                TorchController.CanUseTorch = true;
            }
        }

        void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {                
            if (scene.name == "Menu")
            {
                UIManager.Instance.ShowHUDGameplay(false);
                _player.SetActive(false);
                _pauseManager.SetActive(false);
                CursorController.UnlockCursor();
                _wasInSceneGameplay = false; // reseta a flag ao entrar no menu
            }
            else // gameplay
            {
                if (_wasInSceneGameplay) return; // evita reativar se ja estava na cena de gameplay
                Debug.Log("<color=blue>Entered gameplay scene</color>");
                UIManager.Instance.ShowHUDGameplay(true);
                _player.SetActive(true);
                _pauseManager.SetActive(true);
                CursorController.LockCursor();
                _wasInSceneGameplay = true;
            }
        }

    }

}