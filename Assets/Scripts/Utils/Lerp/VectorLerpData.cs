using UnityEngine;

namespace syscrawl.Utils.Lerp
{
    public class VectorLerpData : ILerpData<Vector3>
    {
        public Vector3 From { get; set; }

        public Vector3 To { get; set; }

        public Vector3 Current { get; set; }

        public Vector3 Evaluate(float t)
        {
            return Vector3.Lerp(From, To, t);
        }
    }
}

