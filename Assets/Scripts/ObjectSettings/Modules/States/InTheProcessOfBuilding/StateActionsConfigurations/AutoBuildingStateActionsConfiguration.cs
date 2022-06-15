namespace ObjectSettings.Modules.States.InTheProcessOfBuilding.StateActionsConfigurations
{
    /// <summary>
    /// Automatically builds the module at a certain speed.
    /// </summary>
    public sealed class AutoBuildingStateActionsConfiguration : InTheProcessOfBuildingModuleStateActionsConfiguration
    {
        protected override void InitializeInstruction()
        {

        }

        public override void EnterAction()
        {
            AttachedModuleObject.ChangeHealth(-AttachedModuleObject.Health);
        }

        public override void UpdateAction()
        {
            AttachedModuleObject.ChangeHealth(0.05f);
        }

        public override void ExitAction()
        {

        }
    }
}