using Player47.Core;
using Player47.Domain.Totem;
using UnityEngine;

namespace Player47.Domain.Mission
{
    public class MissionDetectEndMission : MonoBehaviour, ICondition
    {
        bool wasDeactivatedAllTotems = false;

        void OnEnable()
        {
            TotemManager.Instance.AllTotemsDeactivated.AddListener(() =>
            {
                wasDeactivatedAllTotems = true;
            });
        }

        public bool IsMet()
        {
            return wasDeactivatedAllTotems;
        }
    }

}