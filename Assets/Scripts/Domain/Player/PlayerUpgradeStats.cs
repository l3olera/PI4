using Player47.Domain.Weapon.Abstractions;
using UnityEngine;

namespace Player47.Domain.Player
{
    public class PlayerUpgradeStats : MonoBehaviour
    {
        [SerializeField] PlayerHealth m_health;
        [SerializeField] MonoBehaviour m_WeaponComponent;
        [SerializeField] TorchController m_torchController;

        IWeapon m_weapon;

        void Start() => m_weapon = m_WeaponComponent as IWeapon;

        public void UpgradeMaxHealth(int amount) => m_health.IncreaseMaxHealth(amount);

        public void UpgradeDamage(int amount) => m_weapon.IncreaseDamage(amount);
        public void UpgradeFireRate(float amount) => m_weapon.IncreaseFireRate(amount);

        public void UpgradeTorchRange(float perc) => m_torchController.IncreaseRange(perc);

        public int TorchRangeLevel = 0;

        public int WeaponDamageLevel = 0;
        public int WeaponFireRateLevel = 0;
    }

}