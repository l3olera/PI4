using Player47.Core;
using System;
using UnityEngine;

namespace Player47.Domain.Tutorials
{
    [RequireComponent(typeof(Collider),typeof(Rigidbody))]
    public class TutorialTriggerZone : MonoBehaviour, ITutorialTrigger
    {
        public event Action Triggered;

        Collider m_collider;
        Rigidbody m_rigidbody;

        void Awake()
        {
            m_collider = GetComponent<Collider>();
            m_collider.isTrigger = true;

            m_rigidbody = GetComponent<Rigidbody>();
            m_rigidbody.useGravity = false;
            m_rigidbody.isKinematic = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Triggered?.Invoke();
                m_collider.enabled = false;
            }
        }
    }
}