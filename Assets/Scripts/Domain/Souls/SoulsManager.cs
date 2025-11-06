using Player47.Core.Service;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

namespace Player47.Domain.Souls
{
    /// <summary>
    /// Define e gerencia a quantidade de almas do jogador. Assim como, inscreve e desinscreve ouvintes para a mudança do valor.
    /// </summary>
    public class SoulsManager : MonoBehaviour
    {
        public static SoulsManager Instance { get; private set; }

        [SerializeField] Observer<int> _souls = new(0);

        public int Souls => _souls.Value;

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            // Código de teste para adicionar almas pressionando a tecla "K"
            if(Input.GetKeyDown(KeyCode.K))
            {
                AddSouls(100);
            }
        }
#endif
        public void AddSouls(int amount)
        {
            _souls.Value += amount;
        }

        public void RemoveSouls(int amount)
        {
            _souls.Value -= amount;

            if(_souls.Value < 0)
            {
                _souls.Value = 0;
            }
        }

        public bool CanAfford(int cost)
        {
            return _souls.Value >= cost;
        }

        // Métodos para observadores se inscreverem
        public void AddSoulsListener(UnityAction<int> callback)
        {
            _souls.AddListener(callback);
        }

        public void RemoveSoulsListener(UnityAction<int> callback)
        {
            _souls.RemoveListener(callback);
        }
    }
}
