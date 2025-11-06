using Player47.Domain.Player;
using UnityEngine;

namespace Player47.Domain.Upgrade.Actions
{
    public class TorchRangeUpgrade : MonoBehaviour, IUpgradeAction
    {
        public IDUpgradeAction IdUpgrade => IDUpgradeAction.Range;
        public string KeyUpgradeName => "Aumentar Alcance da Tocha";
        public int Cost => 100;

        public bool CanUpgrade(PlayerUpgradeStats player)
        {
            return player.TorchRangeLevel < 2;
        }

        public void ApplyUpgrade(PlayerUpgradeStats player)
        {
            player.TorchRangeLevel++;
            player.UpgradeTorchRange(1.2f);
        }
    }
}