using UnityEngine;

namespace MidiaAdapt.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        void Awake()
        {
           if(Instance == null)
           {
               DontDestroyOnLoad(gameObject); // Keep this instance across scenes
           }
           else
           {
               Destroy(gameObject); // Ensure only one instance exists
           }

           Instance = this; // Set the singleton instance
        }
    }
}
