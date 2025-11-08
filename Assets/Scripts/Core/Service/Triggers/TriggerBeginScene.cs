using System;
using UnityEngine;

namespace Player47.Core.Service.Triggers
{
    public class TriggerBeginScene : MonoBehaviour, IEventSource
    {
        public event Action Triggered;
        bool _wasTriggered;

        void Start()
        {
            LocalizationManager.OnFinishTranslations += TriggerEvent;
            TriggerEvent();
        }

        void OnDisable()
        {
            LocalizationManager.OnFinishTranslations -= TriggerEvent;
        }

        void TriggerEvent()
        {
            if (_wasTriggered) return;

            Triggered?.Invoke();
            _wasTriggered = true;
        }
    }
}