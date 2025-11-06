using Player47.Core.Managers;
using Player47.Core.Service;
using Player47.Domain.Player;
using Player47.Domain.Souls;
using Player47.Domain.Upgrade.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace Player47.Domain.Upgrade
{
    public abstract class UpgradeUI : MonoBehaviour
    {
        [SerializeField] protected GameObject panel;
        [SerializeField] protected PlayerUpgradeStats player;

        protected UpgradeStation currentStation;

        protected List<IUpgradeAction> m_upgradesAction;

        protected virtual void Start()
        {
            player = FindFirstObjectByType<PlayerUpgradeStats>();
            panel.SetActive(false);
        }

        protected IUpgradeAction GetUpgradeById(IDUpgradeAction id)
        {
            if (m_upgradesAction != null && m_upgradesAction.Count > 0)
            {
                foreach (var action in m_upgradesAction)
                {
                    if (action.IdUpgrade == id)
                    {
                        return action;
                    }
                } 
            }

            return null;
        } 

        public virtual void Open(UpgradeStation station)
        {
            currentStation = station;
            m_upgradesAction = currentStation.GetUpgrades();

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