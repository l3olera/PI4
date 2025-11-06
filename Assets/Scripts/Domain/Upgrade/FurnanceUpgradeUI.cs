using Player47.Domain.Player;
using Player47.Domain.Upgrade.Actions;
using Player47.UI;
using TMPro;
using UnityEngine;

namespace Player47.Domain.Upgrade
{
    public class FurnanceUpgradeUI : UpgradeUI
    {
        [Header("Componentes de Range")]
        [SerializeField] TextMeshProUGUI m_textCurrentRange;

        [Header("Componentes do Custo")]
        [SerializeField] TextMeshProUGUI m_textButtonRange;
        [SerializeField] string m_keyButtonRange;

        IUpgradeAction m_upgradeActionRange;

        public override void Open(UpgradeStation station)
        {
            base.Open(station);

            TextUISmartStringSetter.Instance.SetText(m_textButtonRange, m_keyButtonRange, m_upgradeActionRange.Cost);
        }

        public void TryUpgradeRange()
        {
            TryUpgrade(m_upgradeActionRange);
        }

        protected override void UpdateUI()
        {
            if(m_upgradeActionRange == null)
                m_upgradeActionRange = GetUpgradeById(IDUpgradeAction.Range);

            m_textCurrentRange.SetText(TorchController.Range.ToString());

            TextUISmartStringSetter.Instance.SetText(m_textButtonRange, m_keyButtonRange, m_upgradeActionRange.Cost);
        }
    }

}