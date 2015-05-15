using UnityEngine;

namespace syscrawl.Utils.Lerp
{
    public class LerpSettings
    {
        public float PercentThreshold { get; set; }

        public float Duration { get; set; }

        public AnimationCurve Curve { get; set; }

        public LerpSettings()
        {
            PercentThreshold = 0.99f;
            Duration = 1f;
            Curve = AnimationCurve.Linear(0, 0, 1, 1);
        }
    }
}

