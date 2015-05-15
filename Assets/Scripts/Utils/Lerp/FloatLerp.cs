using UnityEngine;

namespace syscrawl.Utils.Lerp
{
    public class FloatLerp : Lerp<float>
    {
        public FloatLerp(LerpSettings settings = null)
            : base(settings)
        {
            Data = new LerpData();
        }

        class LerpData : ILerpData<float>
        {
            public float From { get; set; }

            public float To { get; set; }

            public float Current { get; set; }

            public float Evaluate(float t)
            {
                return Mathf.Lerp(From, To, t);
            }
        }
    }
}
