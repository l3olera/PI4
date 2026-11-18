using UnityEngine;

namespace Player47.Core
{
    public class SpawnPoint : MonoBehaviour
    {
        public static SpawnPoint Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this && Instance.gameObject.scene == gameObject.scene)
            {
                if (transform.position.y > Instance.transform.position.y)
                {
                    Instance = this;
                }
                return;
            }

            Instance = this;
        }

        public void RespawnPlayerAtPosition(Transform playerTransform)
        {
            if (playerTransform == null)
            {
                Debug.LogWarning("RespawnPlayerAtPosition called with a null playerTransform.");
                return;
            }

            CharacterController characterController = playerTransform.GetComponent<CharacterController>();
            if (characterController == null)
            {
                characterController = playerTransform.GetComponentInChildren<CharacterController>(true);
            }

            bool wasControllerEnabled = false;
            if (characterController != null)
            {
                wasControllerEnabled = characterController.enabled;
                characterController.enabled = false;
            }

            // Define tanto posição quanto rotação
            Debug.Log($"Respawning player at spawn point ({name}) at {transform.position}.");
            Transform targetTransform = characterController != null ? characterController.transform : playerTransform;
            targetTransform.SetPositionAndRotation(transform.position, transform.rotation);
            Physics.SyncTransforms();

            if (characterController != null)
            {
                characterController.enabled = wasControllerEnabled;
            }
        }
    }
}
