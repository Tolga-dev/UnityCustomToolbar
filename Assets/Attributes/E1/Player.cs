using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Attributes.E1
{
    public class Player : MonoBehaviour
    {
        [TextArea(10, 20)] 
        public string description;

        [SerializeField] private int iamAPrivate = 31;

        [HideInInspector] public int iamAPublic;
        
        [field: SerializeField] public int MyProperty { get; set; }

        [Range(0, 360)] public int degree;

        [Header("Hello World!")] public float help;

        [Space] [Tooltip("Gravity is not hot like you")]
        public float gravity;

        public Foo foo;
    }

    [Serializable]
    public class Foo
    {
        public string foo;
    }
}