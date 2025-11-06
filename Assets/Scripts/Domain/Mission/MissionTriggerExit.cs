using Player47.Core;
using Player47.Core.Service.Triggers;
using System;
using UnityEngine;

namespace Player47.Domain.Mission
{
    public class MissionTriggerExit : TriggerSetup, IEventSource
    {
        public event Action Triggered;

        void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Triggered?.Invoke();
                foreach (Collider col in m_colliders)
                {
                    col.enabled = false;
                }
            }
        }
    }

}