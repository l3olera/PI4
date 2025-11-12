using System;
using UnityEngine;

namespace Player47.Core.Service.Triggers
{
    public class TriggerBeginScene : MonoBehaviour, IEventSource
    {
        public event Action Triggered;
        bool m_wasTriggeredByLocalization;

        void Start()
        {
            LocalizationManager.OnFinishTranslations += TriggerEvent;
            TriggerEvent();
        }

        void OnDisable()
        {
            LocalizationManager.OnFinishTranslations -= TriggerEvent;
        }

        void FinishedTranslations()
        {
            TriggerEvent();
            m_wasTriggeredByLocalization = true;
        }

        void TriggerEvent()
        {
            if (m_wasTriggeredByLocalization) return;
            Debug.Log("TriggerBeginScene triggered");
            Triggered?.Invoke();
        }
    }
}