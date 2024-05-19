using System;
using UnityEngine;

namespace PropertyAttribute.E2
{
    public class ForceInterfaceAttribute : UnityEngine.PropertyAttribute
    {
        public readonly Type InterfaceType;

        public ForceInterfaceAttribute(Type interfaceType)
        {
            this.InterfaceType = interfaceType;
        }

    }
}