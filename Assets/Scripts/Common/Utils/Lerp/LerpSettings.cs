using UnityEngine;
using System;

namespace syscrawl.Common.Utils.Lerp
{
    [Serializable]
    public class LerpSettings
    {
        public float PercentThreshold = 0.99f;

        public float Duration = 1f;

        public AnimationCurve Curve = AnimationCurve.Linear(0, 0, 1, 1);
    }
}

