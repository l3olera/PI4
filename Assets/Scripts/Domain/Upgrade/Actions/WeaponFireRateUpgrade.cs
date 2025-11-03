using Player47.Domain.Player;
using UnityEngine;

namespace Player47.Domain.Upgrade.Actions
{
    public class WeaponFireRateUpgrade : MonoBehaviour, IUpgradeAction
    {
        public IDUpgradeAction IdUpgrade => IDUpgradeAction.FireRate;
        public string KeyUpgradeName => "Aumentar Cadência de Tiro";
        public int Cost => 75;

        public bool CanUpgrade(PlayerUpgradeStats player)
        {
            return player.WeaponFireRateLevel < 5;
        }

        public void ApplyUpgrade(PlayerUpgradeStats player)
        {
            player.WeaponFireRateLevel++;
            player.UpgradeFireRate(1.10f);
        }
    }
}