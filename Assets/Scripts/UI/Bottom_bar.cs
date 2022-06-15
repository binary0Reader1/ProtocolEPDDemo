using Factories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Operations;
using FactoryObjects.TileObjects.Modules;

public class Bottom_bar : MonoBehaviour
{
    private ModulesFactory _modulesFactory;

    private void Start()
    {
        _modulesFactory = FindObjectOfType<ModulesFactory>();
    }

    public void PlaceModule(int moduleIndex)
    {
        switch (moduleIndex)
        {
            case 0:
                _modulesFactory.CreateObject(_modulesFactory.GetObject(ModuleObjectType.DroneBuilder));
                break;

            case 1:
                _modulesFactory.CreateObject(_modulesFactory.GetObject(ModuleObjectType.MaterialExtractor));
                break;

            case 2:
                _modulesFactory.CreateObject(_modulesFactory.GetObject(ModuleObjectType.EnergyTransmitter));
                break;

            case 3:
                _modulesFactory.CreateObject(_modulesFactory.GetObject(ModuleObjectType.Rocket));
                break;
        }
    }
}
