using UnityEngine;

namespace Player47.Core
{
    public class SpawnPoint : MonoBehaviour
    {
        public static SpawnPoint Instance { get; private set; }

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        public void RespawnPlayerAtPosition(Transform playerTransform)
        {
            Debug.Log("Ele setou a posição e rotação do player");
            playerTransform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
    
}