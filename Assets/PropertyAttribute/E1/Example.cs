using UnityEngine;

namespace PropertyAttribute.E1
{
    public class Example : MonoBehaviour
    {
        [Randomize(0f, 10f)]
        public float value;
        
    }
}