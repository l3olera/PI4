using UnityEngine;
using TMPro;
using MidiaAdapt.Core.Managers;

namespace MidiaAdapt.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _text_adviseReload;
        [SerializeField] TextMeshProUGUI _text_reload;

        void OnEnable()
        {
            PlayerWeaponManager.BecomeReloading += WeaponManager_ShowAdviseReload;
            PlayerWeaponManager.FinishedReloading += WeaponManager_HideAdviseReload;
            PlayerWeaponManager.AmmoChanged += WeaponManager_AmmoChanged;
        }

        void OnDisable()
        {
            PlayerWeaponManager.BecomeReloading -= WeaponManager_ShowAdviseReload;
            PlayerWeaponManager.FinishedReloading -= WeaponManager_HideAdviseReload;
            PlayerWeaponManager.AmmoChanged -= WeaponManager_AmmoChanged;
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
                _text_reload.text = $"{amount} / {PlayerWeaponManager.Instance.playerCurrentEquippedWeapon?.scoreMaxAmmo}";
            }
        }
    }

}