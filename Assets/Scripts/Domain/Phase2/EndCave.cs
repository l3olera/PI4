using Player47.Core;
using Player47.Core.Service;
using Player47.Core.Service.Triggers;
using UnityEngine;

namespace Player47.Domain.Phase2
{
    public class EndCave : MonoBehaviour, IInteractable
    {
        public string nextSceneName;

        public string TranslateKey => "InteractDoor";

        public bool CanShowText => true;

        public void Interact()
        {
            SceneLoader.Instance.LoadSceneAsync(nextSceneName);
        }
    }
}
