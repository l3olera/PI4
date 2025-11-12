using Player47.Core;
using Player47.Core.Service.Triggers;
using System;
using UnityEngine;

namespace Player47.Domain.Mission
{
    public class MissionTriggerExit : TriggerSetup, IEventSource
    {
        public event Action Triggered;

        public bool canDisableCollidersAfterTrigger = true;

        void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Triggered?.Invoke();
                if(canDisableCollidersAfterTrigger)
                    foreach (Collider col in m_colliders)
                    {
                        col.enabled = false;
                    }
            }
        }
    }

}