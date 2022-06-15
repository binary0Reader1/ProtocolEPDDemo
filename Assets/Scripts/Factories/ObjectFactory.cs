using UnityEngine;
using System.Collections.Generic;
using FactoryObjects;

namespace Factories
{
    ///<Summary>
    ///Base class for all object factories.
    ///Used to standardize the creation of objects.
    ///</Summary>
    public abstract class ObjectFactory<O, T> : MonoBehaviour where O : FactoryObject where T : FactoryObjectType
    {
        /// <summary>
        /// Used to get all the objects that the factory contains.
        /// </summary>
        /// <param name="objectType">Object type from the list of types available to the factory.</param>
        /// <returns>Instance of the requested prefab as a script describing it.</returns>
        public abstract O GetObject(T objectType);

        public abstract void CreateObject(O objectInstance);

/*        /// <summary>
        /// Stores a reference to the list of objects available to the factory.
        /// </summary>
        public abstract T ObjectTypes { get; protected set; }*/

        [Header("All objects stored by the factory.")] [SerializeField] protected O[] objectsHandler;
    }
}