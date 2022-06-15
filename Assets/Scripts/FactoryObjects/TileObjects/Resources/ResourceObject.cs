using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryObjects.Resources
{
    ///<Summary>
    ///Base class for all resource objects.
    ///</Summary>
    public class ResourceObject : FactoryObject
    {

    }

    public class ResourceObjectType : FactoryObjectType
    {
        public ResourceObjectType(string typeKeyWord) : base(typeKeyWord)
        {

        }

        ///All kinds of resources are listed here

        public static readonly ResourceObjectType SmallIron = new ResourceObjectType("ID_SmallIron");
        public static readonly ResourceObjectType BigIron = new ResourceObjectType("ID_BigIron");
    }
}