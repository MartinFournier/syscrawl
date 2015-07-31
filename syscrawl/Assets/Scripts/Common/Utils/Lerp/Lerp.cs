using System;

namespace syscrawl.Common.Utils.Lerp
{
    public abstract class Lerp<T>
    {
        public delegate void LerpActivatedEventHandler();

        public event LerpActivatedEventHandler LerpActivated;

        public delegate void LerpCompletedEventHandler();

        public event LerpCompletedEventHandler LerpCompleted;

        public delegate void LerpUpdatedEventHandler();

        public event LerpUpdatedEventHandler LerpUpdated;

        protected ILerpData<T> Data { get; set; }

        float currentTime;
        float progressPercent;
        Action<T> lerpTarget;

        public bool IsComplete { get; private set; }

        public LerpSettings Settings;

        protected Lerp(LerpSettings settings)
        {
            IsComplete = true;

            if (settings != null)
            {
                this.Settings = settings;
            }
            else
            {
                settings = new LerpSettings();
            }
        }

        public void Activate(T from, T to, Action<T> updateMethod)
        {
            this.lerpTarget = updateMethod;

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

            if (lerpTarget != null)
                lerpTarget.Invoke(result);

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
                    LerpCompleted();
            }
                
            if (LerpUpdated != null)
                LerpUpdated();
        }
    }
}

