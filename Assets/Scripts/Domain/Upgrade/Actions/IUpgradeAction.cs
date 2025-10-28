using Player47.Domain.Player;

namespace Player47.Domain.Upgrade.Actions
{
    public interface IUpgradeAction
    {
        string KeyUpgradeName { get; }
        int Cost { get; }
        bool CanUpgrade(PlayerUpgradeStats player);
        void ApplyUpgrade(PlayerUpgradeStats player);
    }
}