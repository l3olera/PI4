using Player47.Core;
using System;
using UnityEngine;

namespace Player47.Domain.Tutorials
{
    public class TutorialBeginPhase1 : MonoBehaviour, ITutorialTrigger
    {
        public event Action Triggered;
        bool m_wasTrigger;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !m_wasTrigger)
            {
                Triggered?.Invoke();
                m_wasTrigger = true;
            }
        }
    }

}