using System;
using UnityEngine;
using Operations;
using FactoryObjects.TileObjects.Modules;

namespace Factories
{
    public class ModulesFactory : ObjectFactory<ModuleObject, ModuleObjectType>
    {
        private ModuleBuildingOperation _moduleBuildingOperation;
        public override ModuleObject GetObject(ModuleObjectType moduleType)
        {
            string objectName = moduleType.Key;

            if (objectName == ModuleObjectType.DroneBuilder.Key)
            {
                return objectsHandler[0];
            }

            if (objectName == ModuleObjectType.MaterialExtractor.Key)
            {
                return objectsHandler[1];
            }

            if (objectName == ModuleObjectType.EnergyTransmitter.Key)
            {
                return objectsHandler[2];
            }

            if (objectName == ModuleObjectType.Rocket.Key)
            {
                return objectsHandler[3];
            }


            throw new Exception("Invalid object type");
        }

        public override void CreateObject(ModuleObject objectInstance)
        {
            _moduleBuildingOperation.StartPlacingModule(objectInstance);
        }

        private void Awake()
        {
            _moduleBuildingOperation = FindObjectOfType<ModuleBuildingOperation>();
        }
    }
}