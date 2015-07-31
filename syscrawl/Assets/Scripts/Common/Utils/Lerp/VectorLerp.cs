using UnityEngine;

namespace syscrawl.Common.Utils.Lerp
{
    public class VectorLerp : Lerp<Vector3>
    {
        public VectorLerp(LerpSettings settings = null)
            : base(settings)
        {
            Data = new LerpData();
        }

        class LerpData : ILerpData<Vector3>
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
}
