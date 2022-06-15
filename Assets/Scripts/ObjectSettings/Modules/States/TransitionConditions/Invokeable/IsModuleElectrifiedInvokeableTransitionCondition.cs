using GameSystems.Electrification;
using System;


namespace ObjectSettings.Modules.States.TransitionConditions.Invokeable
{
    /// <summary>
    /// Makes the transition if the module is electrified.
    /// </summary>
    public sealed class IsModuleElectrifiedInvokeableTransitionCondition : InvokeableModuleStateTransitionCondition
    {
        private ElectrificationSystem _electrificationSystem;
        public IsModuleElectrifiedInvokeableTransitionCondition(ModuleStateType requestedStateType) : base(requestedStateType)
        {
        }

        protected override ref Action GetActionToSubcsribeInstruction()
        {
            return ref _electrificationSystem.OnElectrificationListChanged;
        }

        protected override void InitializeInstruction()
        {
            _electrificationSystem = UnityEngine.Object.FindObjectOfType<ElectrificationSystem>();
        }

        protected override bool IsConditionPerformedInstruction()
        {
            if (_electrificationSystem.IsTileObjectElectrificated(AttachedModuleObject)) return true;
            return false;
        }
    }
}