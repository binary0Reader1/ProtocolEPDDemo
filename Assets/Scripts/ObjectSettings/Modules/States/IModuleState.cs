using FactoryObjects.TileObjects.Modules;
using System;

namespace ObjectSettings.Modules.States
{
    /// <summary>
    /// Surface interface for generalized interaction with states.
    /// </summary>
    public interface IModuleState
    {
        public void Initialize(Action<IModuleState> setTransitonAction, ModuleObject moduleObject);
        public void EnterAction();
        public void UpdateAction();
        public void ExitAction();
    }
}