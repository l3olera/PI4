using Player47.Core;
using Player47.Core.Service;
using System;
using System.Collections;
using UnityEngine;

namespace Player47.Domain.Phase2
{
    public class EndCave : MonoBehaviour, IInteractable
    {
        public string nextSceneName;

        public string TranslateKey => "InteractDoor";

        public bool CanShowText => true;

        [SerializeField] MonoBehaviour audioEmitterComponent;
        IAudioEmitter audioEmitter;

        void Awake()
        {
            if (audioEmitterComponent != null)
                audioEmitter = audioEmitterComponent as IAudioEmitter;
            else
                audioEmitter = GetComponent<IAudioEmitter>();
        }


        public void Interact()
        {
            audioEmitter?.PlaySound();

            StartCoroutine(LoadSceneAfterAudio());
            
        }

        IEnumerator LoadSceneAfterAudio()
        {
            yield return new WaitUntil(() => audioEmitter == null || !audioEmitter.IsPlaying);

            SceneLoader.Instance.LoadSceneAsync(nextSceneName);
        }
    }
}
