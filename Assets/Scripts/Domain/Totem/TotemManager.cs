using UnityEngine;
using UnityEngine.Events;

namespace Player47.Domain.Totem
{
    public class TotemManager : MonoBehaviour
    {
        public static TotemManager Instance { get; private set; }

        [SerializeField] int m_totalTotems = 4;
        public int TotalTotems => m_totalTotems;

        public int CurrentDeactivatedTotems { get; private set; } = 0;

        public UnityEvent AllTotemsDeactivated = new();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void DeactivateTotem()
        {
            if(CurrentDeactivatedTotems >= TotalTotems) return;
            
            Debug.Log("Totem desativado.");

            CurrentDeactivatedTotems++;
            if (CurrentDeactivatedTotems >= TotalTotems)
            {
                Debug.Log("Todos os totems foram desativados.");
                AllTotemsDeactivated?.Invoke();
                CurrentDeactivatedTotems = TotalTotems;
            }
        }

        [ContextMenu("Deactivate All Totems")]
        void DeactivateAllTotems()
        {
            CurrentDeactivatedTotems = TotalTotems;
            AllTotemsDeactivated?.Invoke();
        }
    }
}