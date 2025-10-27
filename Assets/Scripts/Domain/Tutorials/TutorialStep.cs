using Player47.Core;
using UnityEngine;

namespace Player47.Domain.Tutorials
{
    public class TutorialStep : MonoBehaviour
    {
        [SerializeField] MonoBehaviour triggerComponent;
        [SerializeField] MonoBehaviour conditionComponent;
        [SerializeField] MonoBehaviour actionComponent;

        ITutorialTrigger trigger;
        ITutorialCondition condition;
        ITutorialAction action;

        void Awake()
        {
            trigger = triggerComponent as ITutorialTrigger;
            condition = conditionComponent as ITutorialCondition;
            action = actionComponent as ITutorialAction;
        }

        void OnEnable()
        {
            if (trigger != null)
                trigger.Triggered += OnTriggered;
        }

        void OnDisable()
        {
            if (trigger != null)
                trigger.Triggered -= OnTriggered;
        }

        void OnTriggered()
        {
            if (condition == null || condition.IsMet())
                action?.Execute();
        }
    }
}
