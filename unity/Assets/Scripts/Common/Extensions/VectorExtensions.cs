using UnityEngine;

namespace syscrawl.Common.Extensions
{
    public static class VectorExtensions
    {
      
        public static Vector3 RotatePointAroundPivot(
            this Vector3 point, Vector3 pivot, Vector3 angles)
        {
            var direction = point - pivot;
            direction = Quaternion.Euler(angles) * direction;
            var finalVector = direction + pivot;
            return finalVector;
        }
    }
}

