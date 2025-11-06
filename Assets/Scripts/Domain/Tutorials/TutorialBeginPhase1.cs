using Player47.Core;
using Player47.Core.Service;
using System;
using UnityEngine;

namespace Player47.Domain.Tutorials
{
    public class TutorialBeginPhase1 : MonoBehaviour, IEventSource
    {
        public event Action Triggered;
        bool _wasTriggered;

        void Start()
        {
            LocalizationManager.OnFinishTranslations += LocalizationManager_OnFinishTranslations;
        }

        void OnDisable()
        {
            LocalizationManager.OnFinishTranslations -= LocalizationManager_OnFinishTranslations;
        }

        void LocalizationManager_OnFinishTranslations()
        {
            if (_wasTriggered) return;

            Triggered?.Invoke();
            _wasTriggered = true;
        }
    }
}