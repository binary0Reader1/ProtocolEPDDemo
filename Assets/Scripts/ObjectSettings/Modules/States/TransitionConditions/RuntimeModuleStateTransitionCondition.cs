namespace ObjectSettings.Modules.States.TransitionConditions
{
    /// <summary>
    /// Transition condition that checked every frame.
    /// </summary>
    public abstract class RuntimeModuleStateTransitionCondition : ModuleStateTransitionCondition
    {
        protected RuntimeModuleStateTransitionCondition(ModuleStateType requestedStateType) : base(requestedStateType)
        {

        }
    }
}
