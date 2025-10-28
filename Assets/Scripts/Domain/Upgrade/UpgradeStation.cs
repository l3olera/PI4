using System.Collections.Generic;
using UnityEngine;
using Player47.Core;
using Player47.Domain.Upgrade.Actions;

namespace Player47.Domain.Upgrade
{
    public class UpgradeStation : MonoBehaviour, IInteractable
    {
        [SerializeField] UpgradeUI m_ui;

        [SerializeField] List<MonoBehaviour> upgradeComponents; // arraste scripts concretos no inspector
        readonly List<IUpgradeAction> _upgrades = new();

        [SerializeField] string m_translateKey;

        public string TranslateKey => m_translateKey;

        public bool CanShowText => true;

        void Awake()
        {
            foreach (var comp in upgradeComponents)
            {
                if (comp is IUpgradeAction upgrade)
                    _upgrades.Add(upgrade);
            }
        }

        public List<IUpgradeAction> GetUpgrades() => _upgrades;

        public void Interact()
        {
            m_ui.Open(this);
        }
    }

}