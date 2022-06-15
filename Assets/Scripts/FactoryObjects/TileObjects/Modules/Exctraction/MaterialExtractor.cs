using ObjectSettings.Modules.PlaceActions;
using ObjectSettings.Modules.PlaceConditions;
using ObjectSettings.Modules.States.Inoperative;
using ObjectSettings.Modules.States.InTheProcessOfBuilding;
using ObjectSettings.Modules.States.Working;
using System.Collections.Generic;

namespace FactoryObjects.TileObjects.Modules.Exctraction
{
    public class MaterialExtractor : ModuleObject
    {
        public override HashSet<ModulePlaceCondition> ModulePlaceConditions { get; protected set; } = new HashSet<ModulePlaceCondition>
        {
            new NoModulesAroundModulePlaceCondition(),
            new NoObjectsUnderModulePlaceCondition()
        };
        public override HashSet<ModulePlaceActionConfiguration> ModulePlaceActionConfigurations { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
        public override InTheProcessOfBuildingModuleState InTheProcessOfBuildingModuleState { get; protected set; }
        public override WorkingModuleState WorkingModuleState { get; protected set; }
        public override InoperativeModuleState InoperativeModuleState { get; protected set; }
    }
}