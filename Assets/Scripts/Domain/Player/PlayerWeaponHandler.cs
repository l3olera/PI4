using UnityEngine;
using System;
using Player47.Domain.Weapon.Abstractions;

namespace Player47.Domain.Player
{
    public class PlayerWeaponHandler : MonoBehaviour
    {
        public static PlayerWeaponHandler Instance { get; private set; }

        public static event Action BecomeReloading; //Evento para notificar quando a arma começou a carregar
        public static event Action FinishedReloading; //Evento para notificar quando a arma terminou de carregar
        public static event Action<int> AmmoChanged; //Evento para notificar quando a munição mudou

        public IWeapon playerCurrentEquippedWeapon;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void OnBecomeReloading()
        {
            BecomeReloading?.Invoke();
        }

        public void OnFinishedReloading()
        {
            FinishedReloading?.Invoke();
        }

        public void OnAmmoChanged(int currentAmmo)
        {
            AmmoChanged?.Invoke(currentAmmo);
        }
    }
}
