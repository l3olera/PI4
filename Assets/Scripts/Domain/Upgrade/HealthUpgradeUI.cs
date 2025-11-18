using Player47.Domain.Player;
using Player47.Domain.Upgrade.Actions;
using Player47.UI;
using TMPro;
using UnityEngine;

namespace Player47.Domain.Upgrade
{
    public class HealthUpgradeUI : UpgradeUI
    {
        [Header("Elementos da UI da vida atual")]
        [SerializeField] TextMeshProUGUI m_textCurrentHealth;

        [Header("Botão da UI do Upgrade")]
        [SerializeField] string m_key;
        [SerializeField] TextMeshProUGUI m_textButtonHealth;

        IUpgradeAction m_upgradeActionHealth;

        public override void Open(UpgradeStation station)
        {
            base.Open(station);

            TextUISmartStringSetter.Instance.SetText(m_textButtonHealth, m_key, m_upgradeActionHealth.Cost);
        }

        public void TryUpgradeHealth()
        {
            TryUpgrade(m_upgradeActionHealth);
        }

        protected override void UpdateUI()
        {
            if (m_upgradeActionHealth == null)
            {
                m_upgradeActionHealth = GetUpgradeById(IDUpgradeAction.Health);
            }

            m_textCurrentHealth.SetText(PlayerHealth.Instance.CurrentHealth.Value.ToString());

            TextUISmartStringSetter.Instance.SetText(m_textButtonHealth, m_key, m_upgradeActionHealth.Cost);
        }
    }
}
