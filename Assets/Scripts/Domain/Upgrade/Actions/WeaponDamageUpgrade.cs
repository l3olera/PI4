using Player47.Domain.Player;
using Player47.Domain.Upgrade.Actions;
using UnityEngine;

namespace Assets.Scripts.Domain.Upgrade.Actions
{
    public class WeaponDamageUpgrade : MonoBehaviour, IUpgradeAction
    {
        public string KeyUpgradeName => "Aumentar Dano da Arma";
        public int Cost => 75;

        public bool CanUpgrade(PlayerUpgradeStats player)
        {
            return player.WeaponDamageLevel < 5;
        }

        public void ApplyUpgrade(PlayerUpgradeStats player)
        {
            player.WeaponDamageLevel++;
            player.UpgradeDamage(2);
        }
    }
}