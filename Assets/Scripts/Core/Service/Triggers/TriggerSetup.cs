using UnityEngine;

namespace Player47.Core.Service.Triggers
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class TriggerSetup : MonoBehaviour
    {
        protected Collider[] m_colliders;
        protected Rigidbody m_rigidbody;

        protected virtual void Awake()
        {
            m_colliders = GetComponents<Collider>();

            if (m_colliders.Length > 0)
            {
                foreach (Collider col in m_colliders)
                {
                    col.isTrigger = true;
                }
            }


            m_rigidbody = GetComponent<Rigidbody>();
            m_rigidbody.useGravity = false;
            m_rigidbody.isKinematic = true;
        }
    } 
}
