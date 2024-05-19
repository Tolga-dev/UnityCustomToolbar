using UnityEngine;

namespace PropertyAttribute.E1
{
    public class RandomizeAttribute : UnityEngine.PropertyAttribute
    {
        public readonly float minValue;
        public readonly float maxValue;

        public RandomizeAttribute(float minValue, float maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }
}