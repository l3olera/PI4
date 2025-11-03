using Player47.Domain.Player;
using System;

namespace Player47.Domain.Upgrade.Actions
{
    [Serializable]
    public enum IDUpgradeAction
    {
        Health,
        Range,
        FireRate,
        Damage
    }

    public interface IUpgradeAction
    {
        IDUpgradeAction IdUpgrade { get; }
        string KeyUpgradeName { get; }
        int Cost { get; }
        bool CanUpgrade(PlayerUpgradeStats player);
        void ApplyUpgrade(PlayerUpgradeStats player);
    }
}