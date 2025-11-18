using Player47.Core;
using Player47.Core.Managers;
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
        [SerializeField] GameObject _torchMesh;
        [SerializeField] Transform m_playerTransform;

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
            if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Credits")
            {
                DeactiveGameplayElements();
                UnableTorch();

                _wasInSceneGameplay = false;
            }
        }

#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                EnableTorch();
            }
        }
#endif

        void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {                
            if (scene.name == "Menu" || scene.name == "Credits")
            {
                DeactiveGameplayElements();
                UnableTorch();

                _wasInSceneGameplay = false; // reseta a flag ao entrar no menu
            }
            else // gameplay
            {
                if (SceneManager.GetActiveScene().name != "Phase1" && !TorchController.CanUseTorch) //Apenas permite que o jogador use a tocha se ele ja tiver desbloqueado ela (Ou seja, apenas a partir da fase 2
                {
                    EnableTorch();
                }

                SpawnPoint.Instance.RespawnPlayerAtPosition(m_playerTransform);


                if (_wasInSceneGameplay) return; // evita reativar se ja estava na cena de gameplay

                Debug.Log("<color=blue>Entered gameplay scene</color>");
                ActiveGameplayElements();
                
                _wasInSceneGameplay = true;
            }
        }

        void DeactiveGameplayElements()
        {
            UIManager.Instance.ShowHUDGameplay(false);
            _player.SetActive(false);
            _pauseManager.SetActive(false);
            InputManager.CanListenGameplayInputs.Value = false;
            CursorController.UnlockCursor();
        }

        void ActiveGameplayElements()
        {
            UIManager.Instance.ShowHUDGameplay(true);
            _player.SetActive(true);
            _pauseManager.SetActive(true);
            InputManager.CanListenGameplayInputs.Value = true;
            CursorController.LockCursor();
        }

        void EnableTorch()
        {
            _torchMesh.SetActive(true);
            TorchController.CanUseTorch = true;
        }

        void UnableTorch()
        {
            _torchMesh.SetActive(false);
            TorchController.CanUseTorch = false;
        }

    }

}