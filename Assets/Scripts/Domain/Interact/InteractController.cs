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

    void OnEnable()
    {
        InputManager.InteractInputPerformed += OnInteractInput;
    }

    void OnDisable()
    {
        InputManager.InteractInputPerformed -= OnInteractInput;
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
        Debug.Log($"{other.name} entrou no trigger");
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            Debug.Log("Entrou no trigger de um interagível");
            interactables.Add(interactable);
        }
    }

    void OnTriggerStay(Collider other)
    {
        //Se estiver dentro do trigger de um interagível, atualiza o texto para o mais próximo
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            if(interactables.Contains(interactable))
            {
                IInteractable closest = GetClosestInteractable();
                if(closest != null)
                {
                    m_interactionUI.ShowInteractionText(closest.TranslateKey);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            interactables.Remove(interactable);
        }

        if(interactables.Count == 0)
        {
            m_interactionUI.HideInteractionText();
        }
    }
}
