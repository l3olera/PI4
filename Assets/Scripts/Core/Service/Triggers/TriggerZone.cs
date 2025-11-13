using System;
using UnityEngine;

namespace Player47.Core.Service.Triggers
{
    public class TriggerZone : TriggerSetup, IEventSource
    {
        public event Action Triggered;
        public bool canDisableCollidersAfterTrigger = true;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Triggered?.Invoke();

                if (canDisableCollidersAfterTrigger)
                    foreach (Collider col in m_colliders)
                    {
                        col.enabled = false;
                    }
            }
        }
    }
}