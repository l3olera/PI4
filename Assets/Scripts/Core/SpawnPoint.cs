using UnityEngine;

namespace Player47.Core
{
    public class SpawnPoint : MonoBehaviour
    {
        public static SpawnPoint Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void RespawnPlayerAtPosition(Transform playerTransform)
        {
            // Define tanto posição quanto rotação
            playerTransform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
}
