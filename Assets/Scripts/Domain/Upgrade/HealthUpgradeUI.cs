using TMPro;
using UnityEngine;
using Player47.Domain.Player;
using Player47.UI;

namespace Player47.Domain.Upgrade
{
    public class HealthUpgradeUI : UpgradeUI
    {
        [Header("Elementos da UI da vida atual")]
        [SerializeField] TextMeshProUGUI m_textCurrentHealth;

        [Header("Botão da UI do Upgrade")]
        [SerializeField] string m_key;
        [SerializeField] TextMeshProUGUI m_textButtonHealth;

        protected override void Start()
        {
            base.Start();
            TextUISmartStringSetter.Instance.SetText(m_textButtonHealth, m_key, currentStation.GetUpgrades()[0].Cost.ToString());
        }

        public override void Open(UpgradeStation station)
        {
            base.Open(station);
            var upgrade = currentStation.GetUpgrades()[0];
            TextUISmartStringSetter.Instance.SetText(m_textButtonHealth, m_key, upgrade.Cost);
        }

        protected override void UpdateUI()
        {
            m_textCurrentHealth.SetText(PlayerHealth.Instance.CurrentHealth.ToString());

            var upgrade = currentStation.GetUpgrades()[0];
            TextUISmartStringSetter.Instance.SetText(m_textButtonHealth, m_key, upgrade.Cost);
        }
    }
}
