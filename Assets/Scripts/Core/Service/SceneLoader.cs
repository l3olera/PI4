using Player47.Core.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player47.Core.Service
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void LoadSceneAsync(string sceneName)
        {
            FadeManager.Instance.FadeToBlack();
            StartCoroutine(LoadSceneCoroutine(sceneName));
        }

        IEnumerator LoadSceneCoroutine(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            // Opcional: impede que a cena seja ativada automaticamente
            // asyncLoad.allowSceneActivation = false;

            // Enquanto a cena estiver carregando
            while (!asyncLoad.isDone)
            {
                // Você pode acessar asyncLoad.progress (0 a 0.9)
                // Debug.Log(asyncLoad.progress);
                yield return null;
            }
            // Cena carregada
            // Se você desativou allowSceneActivation, poderia ativar aqui
            // asyncLoad.allowSceneActivation = true;
        }
    }

}