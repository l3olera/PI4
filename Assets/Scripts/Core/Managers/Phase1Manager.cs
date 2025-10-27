using UnityEngine;
using Player47.Core.Service;

namespace Player47.Core.Managers
{
    public class Phase1Manager : MonoBehaviour
    {
        public static Phase1Manager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void LoadPhase2_1()
        {
            SceneLoader.Instance.LoadSceneAsync("Phase2_1");
        }
    }
}
