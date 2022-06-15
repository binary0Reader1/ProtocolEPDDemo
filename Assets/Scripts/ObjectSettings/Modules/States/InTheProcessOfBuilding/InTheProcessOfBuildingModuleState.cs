using ObjectSettings.Modules.States.TransitionConditions;
using System.Collections.Generic;

namespace ObjectSettings.Modules.States.InTheProcessOfBuilding
{
    /// <summary>
    /// Module initial state.
    /// </summary>
    public sealed class InTheProcessOfBuildingModuleState : ModuleState<InTheProcessOfBuildingModuleStateActionsConfiguration>
    {
        public InTheProcessOfBuildingModuleState(InTheProcessOfBuildingModuleStateActionsConfiguration[] moduleStateConfigurations, InvokeableModuleStateTransitionCondition[] invokeableModuleStateTransitionConditions, RuntimeModuleStateTransitionCondition[] runtimeModuleStateTransitionConditions) : base(moduleStateConfigurations, invokeableModuleStateTransitionConditions, runtimeModuleStateTransitionConditions)
        {
        }

        public override HashSet<InTheProcessOfBuildingModuleStateActionsConfiguration> ModuleStateConfigurations { get; protected set; }
        public override HashSet<InvokeableModuleStateTransitionCondition> InvokeableModuleStateTransitionConditions { get; protected set; }
        public override HashSet<RuntimeModuleStateTransitionCondition> RuntimeModuleStateTransitionConditions { get; protected set; }
    }
}
