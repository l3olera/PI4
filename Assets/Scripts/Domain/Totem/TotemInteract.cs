using Player47.Domain.Enemy.Spawner;
using UnityEngine;

namespace Player47.Domain.Totem
{
    /// <summary>
    /// Responsável por controlar o comportamento de interação do totem.
    /// </summary>
    public class TotemInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] Animator _anim;
        [SerializeField] EnemySpawnerTotem _spawner;

        void Awake()
        {
            if(_anim == null)
            {
                _anim = GetComponent<Animator>();
            }

            if(_spawner == null)
            {
                _spawner = GetComponent<EnemySpawnerTotem>();
            }
        }

        public void Interact() { 
            _anim.SetTrigger("Disable");
            _spawner.DisableTotem();
        }

    }
}
