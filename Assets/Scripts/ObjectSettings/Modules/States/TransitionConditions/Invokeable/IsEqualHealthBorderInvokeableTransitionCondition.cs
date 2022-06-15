using System;

namespace ObjectSettings.Modules.States.TransitionConditions.Invokeable
{
    /// <summary>
    /// Makes transition if module's health is equal specified health border.
    /// </summary>
    public sealed class IsEqualHealthBorderInvokeableTransitionCondition : InvokeableModuleStateTransitionCondition
    {
        private readonly float _healthBorder;

        public IsEqualHealthBorderInvokeableTransitionCondition(float healthBorder, ModuleStateType requestedStateType) : base(requestedStateType)
        {
            _healthBorder = healthBorder;
        }

        protected override ref Action GetActionToSubcsribeInstruction()
        {
            return ref AttachedModuleObject.OnHealthChanged;
        }

        protected override void InitializeInstruction()
        {

        }

        protected override bool IsConditionPerformedInstruction()
        {
            if (AttachedModuleObject.Health == _healthBorder)
            {
                return true;
            }

            return false;
        }
    }
}