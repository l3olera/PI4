 using UnityEngine;
using UnityEngine.UI;
using Player47.Domain.Player;

namespace Player47.UI.HUD
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Image _healthBar;

        void OnDisable()
        {
           PlayerHealth.Instance.CurrentHealth.RemoveListener(UpdateBar);    
        }

        void Start()
        {
            PlayerHealth.Instance.CurrentHealth.AddListener(UpdateBar);

            if (_healthBar == null)
            {
                _healthBar = GetComponent<Image>();
            }    
        }

        void UpdateBar(int currentHealth)
        {
            Debug.Log("Entrou no UpdateBar");
            if (_healthBar == null) return;

            _healthBar.fillAmount = (float) currentHealth / PlayerHealth.Instance.MaxHealth;
        }
    } 
}
