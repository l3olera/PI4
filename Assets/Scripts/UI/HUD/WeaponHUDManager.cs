using UnityEngine;
using TMPro;
using Player47.Domain.Player;
using Player47.Core.Service;

namespace Player47.UI.HUD
{
    public class WeaponHUDManager : MonoBehaviour
    {
        [Header("Tradução de textos")]
        public string keyAdviseReload;

        [Header("Textos para referenciar")]
        [SerializeField] TextMeshProUGUI m_textAmmo;
        [SerializeField] TextMeshProUGUI m_textAdviseReload;
        [SerializeField] TextUITranslateSetter m_textTranslateSetter;

        void OnEnable()
        {
            LocalizationManager.OnFinishTranslations += Start;
            PlayerWeaponHandler.BecomeReloading += WeaponManager_ShowAdviseReload;
            PlayerWeaponHandler.FinishedReloading += WeaponManager_HideAdviseReload;
            PlayerWeaponHandler.AmmoChanged += WeaponManager_AmmoChanged;
        }

        void OnDisable()
        {
            LocalizationManager.OnFinishTranslations -= Start;
            PlayerWeaponHandler.BecomeReloading -= WeaponManager_ShowAdviseReload;
            PlayerWeaponHandler.FinishedReloading -= WeaponManager_HideAdviseReload;
            PlayerWeaponHandler.AmmoChanged -= WeaponManager_AmmoChanged;
        }

        void Awake()
        {
            if (m_textTranslateSetter == null)
                m_textTranslateSetter = GetComponent<TextUITranslateSetter>();
        }

        void Start()
        {
           if(m_textAdviseReload != null)
                m_textTranslateSetter.SetTranslateText(m_textAdviseReload, keyAdviseReload);
        }

        public void WeaponManager_ShowAdviseReload()
        {
            m_textAdviseReload.gameObject.SetActive(true);
        }

        public void WeaponManager_HideAdviseReload()
        {
            m_textAdviseReload.gameObject.SetActive(false);
        }

        public void WeaponManager_AmmoChanged(int amount)
        {
            if (m_textAmmo != null)
            {
                Debug.Log("Atualizando munição no HUD");
                m_textAmmo.SetText("{0} / {1}", amount, PlayerWeaponHandler.Instance.playerCurrentEquippedWeapon == null ? amount : (float) PlayerWeaponHandler.Instance.playerCurrentEquippedWeapon.ScoreMaxAmmo);
            }
        }
    }

}