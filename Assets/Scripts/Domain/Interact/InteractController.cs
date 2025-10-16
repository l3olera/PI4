using System.Collections.Generic;
using UnityEngine;
using Player47.Core;
using Player47.Core.Managers;
using Player47.UI.HUD;

/// <summary>
/// Controla a interação do jogador com objetos interativos <see cref="IInteractable"/>
/// </summary>
public class InteractController : MonoBehaviour
{
    readonly List<IInteractable> interactables = new();

    [SerializeField] InteractionUIManager m_interactionUI;

    IInteractable m_lastInteractable = null;
    bool m_lastCanShowText;

    void OnEnable()
    {
        InputManager.InteractInputPerformed += OnInteractInput;
    }

    void OnDisable()
    {
        InputManager.InteractInputPerformed -= OnInteractInput;
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
        if(interactables.Count > 0)
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

        if(interactables.Count == 0)
        {
            m_interactionUI.HideInteractionText();
        }
    }
}
