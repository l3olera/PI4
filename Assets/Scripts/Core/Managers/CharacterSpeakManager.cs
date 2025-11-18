using UnityEngine;
using UnityEngine.Events;

namespace Player47.Core.Managers
{
    public class CharacterSpeakManager : MonoBehaviour
    {
        public static CharacterSpeakManager Instance { get; private set; }

        public UnityEvent DialogueInitiated = new();


        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void InitiateDialogue()
        {
            DialogueInitiated.Invoke();
        }

    }
}