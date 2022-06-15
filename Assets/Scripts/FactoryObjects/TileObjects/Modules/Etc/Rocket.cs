using FactoryObjects.TileObjects.Modules;
using ObjectSettings.Modules.PlaceActions;
using ObjectSettings.Modules.PlaceConditions;
using ObjectSettings.Modules.States;
using ObjectSettings.Modules.States.Inoperative;
using ObjectSettings.Modules.States.InTheProcessOfBuilding;
using ObjectSettings.Modules.States.InTheProcessOfBuilding.StateActionsConfigurations;
using ObjectSettings.Modules.States.TransitionConditions;
using ObjectSettings.Modules.States.TransitionConditions.Invokeable;
using ObjectSettings.Modules.States.Working;
using ObjectSettings.Modules.States.Working.StateActionsConfigurations;
using System.Collections.Generic;

public class Rocket : ModuleObject
{
    public override HashSet<ModulePlaceCondition> ModulePlaceConditions { get; protected set; } = new HashSet<ModulePlaceCondition>()
    {
        new NoObjectsUnderModulePlaceCondition(),
        //new TilesElectrificatedPlaceCondition()
    };

    public override HashSet<ModulePlaceActionConfiguration> ModulePlaceActionConfigurations { get; protected set; } = new HashSet<ModulePlaceActionConfiguration>()
    {
        new DefaultVisulizePlaceConditionsPlaceAction(),
        new ModuleElectrificationVisualizePlaceActionConfiguration(13,13),
        new ElectrificatedTilesVisualizePlaceActionConfiguration()
    };

    public override InTheProcessOfBuildingModuleState InTheProcessOfBuildingModuleState { get; protected set; } = new InTheProcessOfBuildingModuleState(
        new InTheProcessOfBuildingModuleStateActionsConfiguration[]
        {
            new AutoBuildingStateActionsConfiguration()
        },

        new InvokeableModuleStateTransitionCondition[]
        {
            new IsEqualHealthBorderInvokeableTransitionCondition(100, ModuleStateType.Working)
        },

        null
        );
    public override WorkingModuleState WorkingModuleState { get; protected set; } = new WorkingModuleState(
        new WorkingModuleStateActionsConfiguration[]
        {
            new ElectrificationEffectStateActionsConfiguration(13,13)
        },
        null,
        null
        );

    public override InoperativeModuleState InoperativeModuleState { get; protected set; } = new InoperativeModuleState(null, null, null);
}