using Player47.Domain.Player;
using Player47.Domain.Upgrade.Actions;
using Player47.UI;
using TMPro;
using UnityEngine;

namespace Player47.Domain.Upgrade
{
    public class AnvilUpgradeUI : UpgradeUI
    {
        [Header("Elementos da UI dos números de atributos da arma")]
        [SerializeField] TextMeshProUGUI m_textCurrentFireRate;
        [SerializeField] TextMeshProUGUI m_textCurrentDamage;

        [Header("Botão da UI do Upgrade")]
        [SerializeField] string m_keyButtonFireRate;
        [SerializeField] TextMeshProUGUI m_textButtonFireRate;
        [SerializeField] string m_keyButtonDamage;
        [SerializeField] TextMeshProUGUI m_textButtonDamage;

        IUpgradeAction m_upgradeActionFireRate;
        IUpgradeAction m_upgradeActionDamage;

        PlayerWeaponHandler m_weaponHandler;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            base.Start();
            m_weaponHandler = FindFirstObjectByType<PlayerWeaponHandler>();
        }

        public override void Open(UpgradeStation station)
        {
            base.Open(station);

            //Define o texto de cadência
            TextUISmartStringSetter.Instance.SetText(m_textButtonFireRate, m_keyButtonFireRate, m_upgradeActionFireRate.Cost);

            //Define o texto de dano
            TextUISmartStringSetter.Instance.SetText(m_textButtonDamage, m_keyButtonDamage, m_upgradeActionDamage.Cost);
        }

        public void TryUpgradeFireRate()
        {
            TryUpgrade(m_upgradeActionFireRate);
        }

        public void TryUpgradeDamage()
        {
            TryUpgrade(m_upgradeActionDamage);
        }

        protected override void UpdateUI()
        {
            if(m_upgradeActionFireRate == null)
                m_upgradeActionFireRate = GetUpgradeById(IDUpgradeAction.FireRate);

            if(m_upgradeActionDamage == null)
            {
                m_upgradeActionDamage = GetUpgradeById(IDUpgradeAction.Damage);
            }

            //Define o texto atual da cadência
            m_textCurrentFireRate.SetText(m_weaponHandler.playerCurrentEquippedWeapon.FireRate.ToString());

            //Define o texto atual do dano
            m_textCurrentDamage.SetText(m_weaponHandler.playerCurrentEquippedWeapon.Damage.ToString());

            //Define o texto de cadência
            TextUISmartStringSetter.Instance.SetText(m_textButtonFireRate, m_keyButtonFireRate, m_upgradeActionFireRate.Cost);

            //Define o texto de dano
            TextUISmartStringSetter.Instance.SetText(m_textButtonDamage, m_keyButtonDamage, m_upgradeActionDamage.Cost);
        }
    }
}