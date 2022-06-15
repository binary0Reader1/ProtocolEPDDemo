using ObjectSettings.Modules.States.TransitionConditions;
using System.Collections.Generic;

namespace ObjectSettings.Modules.States.Working
{
    /// <summary>
    /// Operating state of the module.
    /// </summary>
    public sealed class WorkingModuleState : ModuleState<WorkingModuleStateActionsConfiguration>
    {
        public WorkingModuleState(WorkingModuleStateActionsConfiguration[] moduleStateConfigurations, InvokeableModuleStateTransitionCondition[] invokeableModuleStateTransitionConditions, RuntimeModuleStateTransitionCondition[] runtimeModuleStateTransitionConditions) : base(moduleStateConfigurations, invokeableModuleStateTransitionConditions, runtimeModuleStateTransitionConditions)
        {
        }

        public override HashSet<WorkingModuleStateActionsConfiguration> ModuleStateConfigurations { get; protected set; }
        public override HashSet<InvokeableModuleStateTransitionCondition> InvokeableModuleStateTransitionConditions { get; protected set; }
        public override HashSet<RuntimeModuleStateTransitionCondition> RuntimeModuleStateTransitionConditions { get; protected set; }
    }
}