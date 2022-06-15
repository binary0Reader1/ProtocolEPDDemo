using FactoryObjects.TileObjects;
using GameSystems.Electrification;
using UnityEngine;

namespace ObjectSettings.Modules.States.Working.StateActionsConfigurations
{
    /// <summary>
    /// Gives the module an electrifying effect.
    /// </summary>
    public sealed class ElectrificationEffectStateActionsConfiguration : WorkingModuleStateActionsConfiguration, IElectrifyEffect
    {
        public TileObject AttachedTileObjectReference { get; set; }
        public int ElecrificationX { get; set; }
        public int ElecrificationZ { get; set; }

        private ElectrificationSystem _electrificationSystem;

        public ElectrificationEffectStateActionsConfiguration(int elecrificationX, int elecrificationZ)
        {
            ElecrificationX = elecrificationX;
            ElecrificationZ = elecrificationZ;
        }

        protected override void InitializeInstruction()
        {
            AttachedTileObjectReference = AttachedModuleObject;
            _electrificationSystem = Object.FindObjectOfType<ElectrificationSystem>();
        }

        public override void EnterAction()
        {
            _electrificationSystem.AddElectrifyEffect(this);
        }

        public override void UpdateAction()
        {

        }

        public override void ExitAction()
        {
            _electrificationSystem.RemoveElectrifyEffect(this);
        }
    }
}