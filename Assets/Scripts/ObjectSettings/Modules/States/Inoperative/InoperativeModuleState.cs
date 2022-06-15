using ObjectSettings.Modules.States.TransitionConditions;
using System.Collections.Generic;

namespace ObjectSettings.Modules.States.Inoperative
{
    /// <summary>
    /// Inoperative state of module.
    /// </summary>
    public sealed class InoperativeModuleState : ModuleState<ModuleStateActionsConfiguration>
    {
        public InoperativeModuleState(ModuleStateActionsConfiguration[] moduleStateConfigurations, InvokeableModuleStateTransitionCondition[] invokeableModuleStateTransitionConditions, RuntimeModuleStateTransitionCondition[] runtimeModuleStateTransitionConditions) : base(moduleStateConfigurations, invokeableModuleStateTransitionConditions, runtimeModuleStateTransitionConditions)
        {
        }

        public override HashSet<ModuleStateActionsConfiguration> ModuleStateConfigurations { get; protected set; }
        public override HashSet<InvokeableModuleStateTransitionCondition> InvokeableModuleStateTransitionConditions { get; protected set; }
        public override HashSet<RuntimeModuleStateTransitionCondition> RuntimeModuleStateTransitionConditions { get; protected set; }
    }
}