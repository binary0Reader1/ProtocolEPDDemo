using FactoryObjects.TileObjects.Modules;

namespace ObjectSettings.Modules.PlaceActions
{
    /// <summary>
    /// Describes the actions that the module will perform during the placement phase.
    /// </summary>
    public abstract class ModulePlaceActionConfiguration
    {
        protected ModuleObject _attachedModuleObject;
        private bool _isInitialized;

        public void Initialize(ModuleObject attachedModuleObject)
        {
            if (_isInitialized) return;

            _attachedModuleObject = attachedModuleObject;
            InitializeInstruction();

            _isInitialized = true;
        }

        /// <summary>
        /// Executed when the module coordinate changes.
        /// </summary>
        public abstract void PlaceAction();

        /// <summary>
        /// Executed when the module is placed.
        /// </summary>
        public abstract void OnPlace();

        /// <summary>
        /// Used to initialize the class.
        /// </summary>
        protected abstract void InitializeInstruction();
    }
}

