using FactoryObjects.TileObjects.Modules;

namespace ObjectSettings.Modules.States
{
    /// <summary>
    /// Describes the actions that the state will perform during own life cycle.
    /// Parrent class for all module state actions configurations.
    /// </summary>
    public abstract class ModuleStateActionsConfiguration
    {
        protected ModuleObject AttachedModuleObject { get; private set; }

        private bool _isInitialized = false;

        public void Initialize(ModuleObject attachedModuleObject)
        {
            if (_isInitialized) return;

            AttachedModuleObject = attachedModuleObject;
            InitializeInstruction();

            _isInitialized = true;
        }

        /// <summary>
        /// Action performed once upon entering the state.
        /// </summary>
        public abstract void EnterAction();

        /// <summary>
        /// Action performed each frame.
        /// </summary>
        public abstract void UpdateAction();

        /// <summary>
        /// Action performed once upon exiting the state.
        /// </summary>
        public abstract void ExitAction();

        /// <summary>
        /// Used to initialize the class.
        /// </summary>
        protected abstract void InitializeInstruction();
    }
}