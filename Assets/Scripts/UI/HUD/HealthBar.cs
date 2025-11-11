 using UnityEngine;
using UnityEngine.UI;
using Player47.Domain.Player;

namespace Player47.UI.HUD
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Image _healthBar;

        void OnEnable()
        {
            if (PlayerHealth.Instance == null) return;
            PlayerHealth.Instance.CurrentHealth.AddListener(UpdateBar);
        }

        void Start()
        {
            OnEnable();

            if (_healthBar == null)
            {
                _healthBar = GetComponent<Image>();
            }    
        }

        void OnDisable()
        {
           PlayerHealth.Instance.CurrentHealth.RemoveListener(UpdateBar);    
        }

        void UpdateBar(int currentHealth)
        {
            if (_healthBar == null) return;

            _healthBar.fillAmount = (float) currentHealth / PlayerHealth.Instance.MaxHealth;
        }
    } 
}
