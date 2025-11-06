using UnityEngine;

namespace Player47.Core.Service
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Trigger : MonoBehaviour
    {
        Collider m_collider;
        Rigidbody m_rigidbody;

        protected virtual void Awake()
        {
            m_collider = GetComponent<Collider>();
            m_collider.isTrigger = true;

            m_rigidbody = GetComponent<Rigidbody>();
            m_rigidbody.useGravity = false;
            m_rigidbody.isKinematic = true;
        }
    } 
}
