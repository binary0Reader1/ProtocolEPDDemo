using FactoryObjects.TileObjects.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectSettings.Modules.States.TransitionConditions.Runtime
{
    public class TestSphereCheckCondition : RuntimeModuleStateTransitionCondition
    {
        private Collider _attachedModuleObjectCollider;

        public TestSphereCheckCondition(ModuleStateType requestedStateType) : base(requestedStateType)
        {
        }

        protected override void InitializeInstruction()
        {
            _attachedModuleObjectCollider = AttachedModuleObject.GetComponent<Collider>();
        }

        protected override bool IsConditionPerformedInstruction()
        {
            //Do smth
            return false;
        }
    }
}