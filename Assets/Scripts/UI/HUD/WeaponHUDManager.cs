using UnityEngine;
using TMPro;
using Player47.Domain.Player;

namespace Player47.UI.HUD
{
    public class WeaponHUDManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _text_adviseReload;
        [SerializeField] TextMeshProUGUI _text_reload;

        void OnEnable()
        {
            PlayerWeaponHandler.BecomeReloading += WeaponManager_ShowAdviseReload;
            PlayerWeaponHandler.FinishedReloading += WeaponManager_HideAdviseReload;
            PlayerWeaponHandler.AmmoChanged += WeaponManager_AmmoChanged;
        }

        void OnDisable()
        {
            PlayerWeaponHandler.BecomeReloading -= WeaponManager_ShowAdviseReload;
            PlayerWeaponHandler.FinishedReloading -= WeaponManager_HideAdviseReload;
            PlayerWeaponHandler.AmmoChanged -= WeaponManager_AmmoChanged;
        }

        public void WeaponManager_ShowAdviseReload()
        {
            _text_adviseReload.gameObject.SetActive(true);
        }

        public void WeaponManager_HideAdviseReload()
        {
            _text_adviseReload.gameObject.SetActive(false);
        }

        public void WeaponManager_AmmoChanged(int amount)
        {
            if (_text_reload != null)
            {
                Debug.Log("Atualizando munição no HUD");
                _text_reload.SetText("{0} / {1}", amount, PlayerWeaponHandler.Instance.playerCurrentEquippedWeapon == null ? amount : (float) PlayerWeaponHandler.Instance.playerCurrentEquippedWeapon.ScoreMaxAmmo);
            }
        }
    }

}