using Player47.Core;
using Player47.Core.Service;
using System;
using UnityEngine;

namespace Player47.Domain.Tutorials
{
    [RequireComponent(typeof(Collider),typeof(Rigidbody))]
    public class TriggerZone : TriggerSetup, IEventSource
    {
        public event Action Triggered;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Triggered?.Invoke();
                foreach(Collider col in m_colliders)
                {
                    col.enabled = false;
                }
            }
        }
    }
}