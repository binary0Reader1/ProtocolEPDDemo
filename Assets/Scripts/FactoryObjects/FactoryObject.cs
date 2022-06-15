using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryObjects
{
    ///<Summary>
    ///Base class for all objects of factories.
    ///</Summary>>
    public class FactoryObject : MonoBehaviour
    {

    }

    ///<Summary>
    ///Base class for all object types lists.
    ///Used as an abstract analogue of enum for object factories.
    ///</Summary>
    public class FactoryObjectType
    {
        public string Key { get; }

        protected FactoryObjectType(string key)
        {
            Key = key;
        }
    }
}