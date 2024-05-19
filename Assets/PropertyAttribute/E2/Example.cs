using UnityEngine;

namespace PropertyAttribute.E2
{
    public class Example : MonoBehaviour
    {
        [ForceInterface(typeof(ICanMove))] 
        public Object canMove;
    }
}