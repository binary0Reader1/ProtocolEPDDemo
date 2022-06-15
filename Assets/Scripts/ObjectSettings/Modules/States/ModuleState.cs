using FactoryObjects.TileObjects.Modules;
using ObjectSettings.Modules.States.TransitionConditions;
using System;
using System.Collections.Generic;

namespace ObjectSettings.Modules.States
{
    /// <summary>
    /// Parrent class for all module states.
    /// </summary>
    public abstract class ModuleState<C> : IModuleState where C : ModuleStateActionsConfiguration
    {
        public abstract HashSet<C> ModuleStateConfigurations { get; protected set; }
        public abstract HashSet<InvokeableModuleStateTransitionCondition> InvokeableModuleStateTransitionConditions { get; protected set; }
        public abstract HashSet<RuntimeModuleStateTransitionCondition> RuntimeModuleStateTransitionConditions { get; protected set; }

        public ModuleState(C[] moduleStateConfigurations, InvokeableModuleStateTransitionCondition[] invokeableModuleStateTransitionConditions, RuntimeModuleStateTransitionCondition[] runtimeModuleStateTransitionConditions)
        {
            //Initializing state's options handlers 
            ModuleStateConfigurations = InitializeHashSet(ModuleStateConfigurations, moduleStateConfigurations);
            InvokeableModuleStateTransitionConditions = InitializeHashSet(InvokeableModuleStateTransitionConditions, invokeableModuleStateTransitionConditions);
            RuntimeModuleStateTransitionConditions = InitializeHashSet(RuntimeModuleStateTransitionConditions, runtimeModuleStateTransitionConditions);
        }

        /// <summary>
        /// Initializing all state's options.
        /// </summary>
        /// <param name="setStateAction">ModuleObject.SetState() action reference</param>
        public void Initialize(Action<IModuleState> setStateAction, ModuleObject attachedModuleObject)
        {
            CompleteCollectionActions((C moduleStateConfiguration)
                => moduleStateConfiguration.Initialize(attachedModuleObject), ModuleStateConfigurations);

            CompleteCollectionActions((InvokeableModuleStateTransitionCondition invokeableModuleStateTransitionCondition)
                => invokeableModuleStateTransitionCondition.Initialize(setStateAction, attachedModuleObject), InvokeableModuleStateTransitionConditions);

            CompleteCollectionActions((RuntimeModuleStateTransitionCondition runtimeModuleStateTransitionCondition)
                => runtimeModuleStateTransitionCondition.Initialize(setStateAction, attachedModuleObject), RuntimeModuleStateTransitionConditions);
        }

        public void EnterAction()
        {
            //Completing all moduleStateConfigurations enter actions
            CompleteCollectionActions((C moduleStateConfiguration)
                => moduleStateConfiguration.EnterAction(), ModuleStateConfigurations);

            //Completing all invokeableModuleStateTransitionCondition subscribtions
            CompleteCollectionActions((InvokeableModuleStateTransitionCondition invokeableModuleStateTransitionCondition)
                => invokeableModuleStateTransitionCondition.SubscribeCondition(), InvokeableModuleStateTransitionConditions);

        }

        public void UpdateAction()
        {
            //Completing all moduleStateConfigurations update actions
            CompleteCollectionActions((C moduleStateConfiguration)
                => moduleStateConfiguration.UpdateAction(), ModuleStateConfigurations);

            //Checking all runtime transition conditions
            CompleteCollectionActions((RuntimeModuleStateTransitionCondition runtimeModuleStateTransitionCondition)
                => runtimeModuleStateTransitionCondition.IsConditionPerformed(), RuntimeModuleStateTransitionConditions);
        }

        public void ExitAction()
        {
            //Completing all moduleStateConfigurations exit actions
            CompleteCollectionActions((C moduleStateConfiguration)
                => moduleStateConfiguration.ExitAction(), ModuleStateConfigurations);

            //Completing all invokeableModuleStateTransitionCondition unsubscribtions
            CompleteCollectionActions((InvokeableModuleStateTransitionCondition invokeableModuleStateTransitionCondition)
                => invokeableModuleStateTransitionCondition.UnsubscribeCondition(), InvokeableModuleStateTransitionConditions);
        }

        private void CompleteCollectionActions<T>(Action<T> moduleConfigurationAction, HashSet<T> requestedCollection)
        {
            foreach (T moduleStateConfiguration in requestedCollection)
            {
                moduleConfigurationAction?.Invoke(moduleStateConfiguration);
            }
        }

        private HashSet<T> InitializeHashSet<T>(HashSet<T> initializedHashSet, T[] valuesArray)
        {
            initializedHashSet = new HashSet<T>();
            if (valuesArray == null)
            {
                return initializedHashSet;
            }

            foreach (T value in valuesArray)
            {
                initializedHashSet.Add(value);
            }

            return initializedHashSet;
        }
    }

    public enum ModuleStateType
    {
        InTheProcessOfBuilding,
        Working,
        Inoperative
    }
}