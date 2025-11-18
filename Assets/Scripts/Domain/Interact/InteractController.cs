using Player47.Core;
using Player47.Core.Managers;
using Player47.UI.HUD;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Controla a interação do jogador com objetos interativos <see cref="IInteractable"/>
/// </summary>
namespace Player47.Domain.Interact
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractController : MonoBehaviour
    {
        readonly List<IInteractable> interactables = new();

        [SerializeField] InteractionUIManager m_interactionUI;

        IInteractable m_lastInteractable = null;
        bool m_lastCanShowText;

        Rigidbody m_rb;

        void OnEnable()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            InputManager.InteractInputPerformed += OnInteractInput;
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Start();
        }

        void OnDisable()
        {
            InputManager.InteractInputPerformed -= OnInteractInput;
        }

        void Awake()
        {
            if (TryGetComponent<Rigidbody>(out var rb))
            {
                m_rb = rb;
                m_rb.isKinematic = true; //O rigidbody é usado apenas para detectar colisões, não para física
                m_rb.useGravity = false;
            }
        }

        void Start()
        {
            interactables.Clear();
            m_lastInteractable = null;
            m_lastCanShowText = false;
            m_interactionUI.HideInteractionText();
        }

        void Update()
        {
            if (interactables.Count == 0) return;

            IInteractable closest = GetClosestInteractable();
            if (closest == null) return;

            if (closest != m_lastInteractable || closest.CanShowText != m_lastCanShowText)
            {
                m_lastInteractable = closest;
                m_lastCanShowText = closest.CanShowText;

                if (closest.CanShowText)
                    m_interactionUI.ShowInteractionText(closest.TranslateKey);
                else
                    m_interactionUI.HideInteractionText();
            }
        }

        void OnInteractInput()
        {
            if (interactables.Count > 0)
            {
                // Interage com o mais próximo
                IInteractable closest = GetClosestInteractable();

                closest?.Interact();
            }
        }

        IInteractable GetClosestInteractable()
        {
            float minDist = Mathf.Infinity;
            IInteractable closest = null;

            foreach (var i in interactables)
            {
                float dist = Vector3.Distance(transform.position, ((MonoBehaviour)i).transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = i;
                }
            }
            return closest;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IInteractable>(out var interactable))
            {
                Debug.Log($"{other.name} entrou no interactable");
                if (!interactables.Contains(interactable))
                    interactables.Add(interactable);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IInteractable>(out var interactable))
            {
                interactables.Remove(interactable);
                m_lastInteractable = null;
                m_lastCanShowText = false;
            }

            if (interactables.Count == 0)
            {
                m_interactionUI.HideInteractionText();
            }
        }
    }

}