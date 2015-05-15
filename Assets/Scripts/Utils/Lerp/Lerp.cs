using UnityEngine;

namespace syscrawl.Utils.Lerp
{
    public abstract class Lerp<T>
    {
        public delegate void LerpActivatedEventHandler();

        public event LerpActivatedEventHandler LerpActivated;

        public delegate void LerpCompletedEventHandler();

        public event LerpCompletedEventHandler LerpCompleted;

        protected ILerpData<T> Data { get; set; }

        float currentTime;
        float progressPercent;

        public bool IsComplete { get; private set; }

        public LerpSettings Settings { get; set; }

        protected Lerp(LerpSettings settings)
        {
            IsComplete = true;

            if (settings != null)
            {
                Settings = settings;
            }
            else
            {
                Settings = new LerpSettings();
            }
        }

        public void Activate(T from, T to)
        {
            currentTime = 0;

            Data.From = from;
            Data.Current = from;
            Data.To = to;

            IsComplete = false;

            if (LerpActivated != null)
            {
                LerpActivated();
            }
        }

        public T Evaluate(float deltaTime)
        {
            UpdateProgress(deltaTime);
            var t = Settings.Curve.Evaluate(progressPercent);
            var result = Data.Evaluate(t);
            Data.Current = result;
            return result;
        }

        void UpdateProgress(float deltaTime)
        {
            if (IsComplete)
                return;
            
            currentTime += deltaTime;
            if (currentTime > Settings.Duration)
            {
                currentTime = Settings.Duration;
            }

            progressPercent = currentTime / Settings.Duration;
            if (progressPercent >= Settings.PercentThreshold)
            {
                progressPercent = 1f;

                IsComplete = true;

                if (LerpCompleted != null)
                {
                    LerpCompleted();
                }
            }
        }
    }
}

