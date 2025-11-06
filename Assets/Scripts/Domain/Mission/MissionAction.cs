using Player47.Core;
using Player47.UI.HUD;
using UnityEngine;

namespace Player47.Domain.Mission
{
    public class MissionAction : MonoBehaviour, IAction
    {
        public string keyTrasnlateMission;

        public void Execute()
        {
            MissionHUDManager.Instance.SetMissionText(keyTrasnlateMission);
        }
    }
}
