using Player47.Core.Service;
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
