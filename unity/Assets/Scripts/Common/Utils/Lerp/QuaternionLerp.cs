using UnityEngine;

namespace syscrawl.Common.Utils.Lerp
{
    public class QuaternionLerp : Lerp<Quaternion>
    {
        public QuaternionLerp(LerpSettings settings = null)
            : base(settings)
        {
            Data = new LerpData();
        }

        class LerpData : ILerpData<Quaternion>
        {
            public Quaternion From { get; set; }

            public Quaternion To { get; set; }

            public Quaternion Current { get; set; }

            public Quaternion Evaluate(float t)
            {
                return Quaternion.Slerp(From, To, t);
            }
        }
    }
}
