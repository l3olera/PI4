using UnityEngine;
using Player47.Core;
using Player47.Domain.Enemy.Spawner;

namespace Player47.Domain.Totem
{
    /// <summary>
    /// Responsável por controlar o comportamento de interação do totem.
    /// </summary>
    public class TotemInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] Animator _anim;
        [SerializeField] EnemySpawnerTotem _spawner;

        [SerializeField] string m_translateKey = "TotemInteract";

        bool _canShowText = true;

        public string TranslateKey => m_translateKey;
        public bool CanShowText => _canShowText;

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
            if(_spawner != null && !_spawner.isActive) return; //Impede o jogador de interagir com o totem se ele já estiver desativado

            _anim.SetTrigger("Disable");
            _spawner.DisableTotem();
            _canShowText = false;
        }
    }
}
