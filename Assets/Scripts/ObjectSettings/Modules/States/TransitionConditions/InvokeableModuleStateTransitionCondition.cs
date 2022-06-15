using System;

namespace ObjectSettings.Modules.States.TransitionConditions
{
    /// <summary>
    /// Transition condition that invokes by action delegate in another class.
    /// </summary>
    public abstract class InvokeableModuleStateTransitionCondition : ModuleStateTransitionCondition
    {
        protected InvokeableModuleStateTransitionCondition(ModuleStateType requestedStateType) : base(requestedStateType)
        {

        }

        /// <summary>
        /// Subscribing condition to selected action delegate.
        /// </summary>
        public void SubscribeCondition()
        {
            ref Action actionToSubscribeReference = ref GetActionToSubcsribeInstruction();
            actionToSubscribeReference += IsConditionPerformed;
        }

        /// <summary>
        /// Unsubscribing condition from selected action delegate.
        /// </summary>
        public void UnsubscribeCondition()
        {
            ref Action actionToSubscribeReference = ref GetActionToSubcsribeInstruction();
            actionToSubscribeReference -= IsConditionPerformed;
        }

        /// <summary>
        /// Instruction for finding the needed Action delegate
        /// </summary>
        protected abstract ref Action GetActionToSubcsribeInstruction();
    }
}