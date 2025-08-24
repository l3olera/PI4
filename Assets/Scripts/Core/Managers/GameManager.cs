using UnityEngine;

namespace MidiaAdapt.Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }else
            {
                Instance = this;
            }

            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
    }
}
