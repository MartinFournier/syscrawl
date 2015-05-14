using UnityEngine;

namespace syscrawl.Utils.Lerp
{
    public abstract class Lerp<T>
    {
        protected ILerpData<T> Data { get; set; }

        float currentTime;
        float progressPercent;

        public bool IsComplete { get; private set; }

        public float PercentThreshold { get; set; }

        public float Duration { get; set; }

        public AnimationCurve Curve { get; set; }

        protected Lerp()
        {
            IsComplete = true;
            PercentThreshold = 0.99f;
            Duration = 1f;
            Curve = AnimationCurve.Linear(0, 0, 1, 1);
        }

        public void Activate(T from, T to)
        {
            currentTime = 0;

            Data.From = from;
            Data.Current = from;
            Data.To = to;

            IsComplete = false;
        }

        public T Evaluate(float deltaTime)
        {
            UpdateProgress(deltaTime);
            var t = Curve.Evaluate(progressPercent);
            var result = Data.Evaluate(t);
            Data.Current = result;
            return result;
        }

        void UpdateProgress(float deltaTime)
        {
            currentTime += deltaTime;
            if (currentTime > Duration)
            {
                currentTime = Duration;
            }

            progressPercent = currentTime / Duration;
            if (progressPercent >= PercentThreshold)
            {
                progressPercent = 1f;

                IsComplete = true;
            }
        }
    }
}

