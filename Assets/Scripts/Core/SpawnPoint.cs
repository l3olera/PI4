using UnityEngine;

namespace Player47.Core
{
    public class SpawnPoint : MonoBehaviour
    {
        public static SpawnPoint Instance { get; private set; }

        void Awake()
        {
            Instance = this;
        }

        public void RespawnPlayerAtPosition(Transform playerTransform)
        {
            // Define tanto posição quanto rotação
            Debug.Log("Respawning player at spawn point.");
            playerTransform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
}
