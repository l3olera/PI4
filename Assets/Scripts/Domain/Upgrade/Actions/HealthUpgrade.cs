using Player47.Domain.Player;
using UnityEngine;

namespace Player47.Domain.Upgrade.Actions
{
    public class HealthUpgrade : MonoBehaviour, IUpgradeAction
    {
        public string KeyUpgradeName => "Aumentar Vida";
        public int Cost => 50; // Exemplo de custo

        public bool CanUpgrade(PlayerUpgradeStats player) => true; // Sem limite

        public void ApplyUpgrade(PlayerUpgradeStats player) => player.UpgradeMaxHealth(10);
    }
}