using ObjectSettings.Modules.PlaceActions;
using ObjectSettings.Modules.PlaceConditions;
using ObjectSettings.Modules.States;
using ObjectSettings.Modules.States.Inoperative;
using ObjectSettings.Modules.States.InTheProcessOfBuilding;
using ObjectSettings.Modules.States.Working;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryObjects.TileObjects.Modules
{
    ///<Summary>
    ///Base class for all module objects.
    ///</Summary>
    public abstract class ModuleObject : TileObject
    {
        //CRUNCH ZONE STARTED
        public Action OnHealthChanged;
        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; } = 100;
        //CRUNCHZONE ENDED

        public abstract HashSet<ModulePlaceCondition> ModulePlaceConditions { get; protected set; }
        public abstract HashSet<ModulePlaceActionConfiguration> ModulePlaceActionConfigurations { get; protected set; }
        public abstract InTheProcessOfBuildingModuleState InTheProcessOfBuildingModuleState { get; protected set; }
        public abstract WorkingModuleState WorkingModuleState { get; protected set; }
        public abstract InoperativeModuleState InoperativeModuleState { get; protected set; }

        private IModuleState _currentState;

        private bool _isStartedPlacing = false;
        private bool _isPlaced = false;

        //CRUNCH ZONE STARTED
        public void StartPlacing()
        {
            if (_isStartedPlacing) return;

            Initialize();

            Health = MaxHealth;

            _isStartedPlacing = true;
        }

        public void PlaceAction()
        {
            if (_isPlaced) return;
            foreach (ModulePlaceActionConfiguration modulePlaceActionConfiguration in ModulePlaceActionConfigurations)
            {
                modulePlaceActionConfiguration.PlaceAction();
            }
        }

        public void Place()
        {
            if (_isPlaced) return;
            foreach (ModulePlaceActionConfiguration modulePlaceActionConfiguration in ModulePlaceActionConfigurations)
            {
                modulePlaceActionConfiguration.OnPlace();
            }

            SetState(InTheProcessOfBuildingModuleState);

            _isPlaced = true;
        }

        public void ChangeHealth(float value)
        {
            Health = Mathf.Clamp(Health + value, -1, MaxHealth);

            if (Health < 0)
            {
                DestroyModule();
            }

            Debug.Log(Health);
            OnHealthChanged?.Invoke();
        }


        private void DestroyModule()
        {

        }

        private void Update()
        {
            if (_currentState != null) _currentState.UpdateAction();
        }
        //CRUNCHZONE ENDED

        private void SetState(IModuleState requestedState)
        {
            if (_currentState != null)
            {
                _currentState.ExitAction();
            }
            _currentState = requestedState;
            _currentState.EnterAction();

            Debug.Log(gameObject.name + " moved to " + _currentState.GetType().Name);
        }

        private void Initialize()
        {
            foreach (ModulePlaceCondition placeCondition in ModulePlaceConditions)
            {
                placeCondition.Initialize(this);
            }

            foreach (ModulePlaceActionConfiguration modulePlaceActionConfiguration in ModulePlaceActionConfigurations)
            {
                modulePlaceActionConfiguration.Initialize(this);
            }

            InTheProcessOfBuildingModuleState.Initialize(SetState, this);
            WorkingModuleState.Initialize(SetState, this);
            InoperativeModuleState.Initialize(SetState, this);
        }

        //Refactor this shit 
        #region CRUNCHEDMaterialManagment
        [SerializeField] private Renderer[] _allColorsCRUNCHED;


        public void SetMiddleTransparentModeCRUNCHED()
        {
            foreach (Renderer renderer in _allColorsCRUNCHED)
            {
                /*Debug.Log(renderer.name);*/
                Color currentColor = renderer.material.color;
                Color defaultColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0.85f);

                renderer.material.color = defaultColor;
            }
        }

        public void SetTransparentModeCRUNCHED()
        {
            foreach (Renderer renderer in _allColorsCRUNCHED)
            {
                Debug.Log(renderer.name);
                Color currentColor = renderer.material.color;
                Color defaultColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0.3f);

                renderer.material.color = defaultColor;
            }
        }

        public void SetDefaultModeCRUNCHED()
        {
            foreach (Renderer renderer in _allColorsCRUNCHED)
            {
                Color currentColor = renderer.material.color;
                Color defaultColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
                renderer.material.color = defaultColor;
            }
        }
        #endregion CRUNCHEDMaterialManagment 

        private void OnDrawGizmos()
        {
            for (int x = 0; x < _xSize; x++)
            {
                for (int z = 0; z < _zSize; z++)
                {
                    Gizmos.color = new Color(0, 1f, 0, 0.32f);
                    Gizmos.DrawCube(transform.position + new Vector3(x, 0, z), new Vector3(1, 0.2f, 1));
                }
            }
        }
    }

    public class ModuleObjectType : FactoryObjectType
    {

        public ModuleObjectType(string typeKeyWord) : base(typeKeyWord)
        {

        }

        ///All kinds of modules are listed here

        public static readonly ModuleObjectType DroneBuilder = new ModuleObjectType("ID_DroneBuilder");
        public static readonly ModuleObjectType MaterialExtractor = new("ID_MaterialExtractor");
        public static readonly ModuleObjectType EnergyTransmitter = new("ID_EnergyTransmitter");
        public static readonly ModuleObjectType Rocket = new("ID_Rocket");
    }
}
