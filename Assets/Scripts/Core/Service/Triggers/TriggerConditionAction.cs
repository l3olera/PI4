using UnityEngine;

namespace Player47.Core.Service.Triggers
{
    public class TriggerConditionAction : MonoBehaviour
    {
        [SerializeField] MonoBehaviour triggerComponent;
        [SerializeField] MonoBehaviour conditionComponent;
        [SerializeField] MonoBehaviour actionComponent;

        IEventSource trigger;
        ICondition condition;
        IAction action;

        void Awake()
        {
            trigger = triggerComponent as IEventSource;
            condition = conditionComponent as ICondition;
            action = actionComponent as IAction;
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
