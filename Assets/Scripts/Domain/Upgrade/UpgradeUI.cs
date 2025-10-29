using Player47.Core.Managers;
using Player47.Core.Service;
using Player47.Domain.Player;
using Player47.Domain.Souls;
using Player47.Domain.Upgrade.Actions;
using Player47.UI;
using TMPro;
using UnityEngine;

namespace Player47.Domain.Upgrade
{
    public abstract class UpgradeUI : MonoBehaviour
    {
        [SerializeField] protected GameObject panel;
        [SerializeField] protected PlayerUpgradeStats player;

        protected UpgradeStation currentStation;


        protected virtual void Start()
        {
            player = FindFirstObjectByType<PlayerUpgradeStats>();
            panel.SetActive(false);
        }

        public virtual void Open(UpgradeStation station)
        {
            currentStation = station;
            panel.SetActive(true);
            UpdateUI();
            PauseManager.CanListenInputPause = false;
            InputManager.CanListenGameplayInputs.Value = false;
            CursorController.UnlockCursor();
        }

        protected abstract void UpdateUI();

        public void TryUpgrade(IUpgradeAction upgrade)
        {
            if (!SoulsManager.Instance.CanAfford(upgrade.Cost)) return;
            if (!upgrade.CanUpgrade(player)) return;

            SoulsManager.Instance.RemoveSouls(upgrade.Cost);
            upgrade.ApplyUpgrade(player);

            UpdateUI();
        }

        public void Close() {
            panel.SetActive(false);
            CursorController.LockCursor();
            InputManager.CanListenGameplayInputs.Value = true;
            PauseManager.CanListenInputPause = true;
        }
    }

}