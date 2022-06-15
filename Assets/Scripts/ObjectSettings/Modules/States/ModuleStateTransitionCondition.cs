using FactoryObjects.TileObjects.Modules;
using System;

namespace ObjectSettings.Modules.States
{
    /// <summary>
    /// Describes the conditions under which a building transitions from one state to another.
    /// Parrent class for all module module state transition conditions.
    /// </summary>
    public abstract class ModuleStateTransitionCondition
    {
        protected ModuleObject AttachedModuleObject { get; private set; }

        private Action<IModuleState> _setTransitionAction; //Reference to ModuleObject.SetTransition() 
        private readonly ModuleStateType _to; //The state to which the transition will be performed
        private bool _isInitialized = false;

        public ModuleStateTransitionCondition(ModuleStateType to)
        {
            _to = to;
        }

        public void Initialize(Action<IModuleState> setTransitonAction, ModuleObject attachedModuleObject)
        {
            if (_isInitialized) return;

            _setTransitionAction = setTransitonAction;
            AttachedModuleObject = attachedModuleObject;

            InitializeInstruction();

            _isInitialized = true;
        }

        public void IsConditionPerformed() //If performed, the transition will be completed
        {
            if (IsConditionPerformedInstruction())
            {
                IModuleState requestedModuleState = null;

                switch (_to) //Getting a reference to the state of attached module object
                {
                    case ModuleStateType.InTheProcessOfBuilding:
                        requestedModuleState = AttachedModuleObject.InTheProcessOfBuildingModuleState;
                        break;

                    case ModuleStateType.Working:
                        requestedModuleState = AttachedModuleObject.WorkingModuleState;
                        break;

                    case ModuleStateType.Inoperative:
                        requestedModuleState = AttachedModuleObject.InoperativeModuleState;
                        break;
                }

                _setTransitionAction.Invoke(requestedModuleState);
            }
        }

        /// <summary>
        /// Used to describe transition condition.
        /// </summary>
        protected abstract bool IsConditionPerformedInstruction();

        /// <summary>
        /// Used to initialize the class.
        /// </summary>
        protected abstract void InitializeInstruction();
    }
}