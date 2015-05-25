using UnityEngine;
using System;

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

        public LerpSettings settings;

        protected Lerp(LerpSettings settings)
        {
            IsComplete = true;

            if (settings != null)
            {
                this.settings = settings;
            }
            else
            {
                settings = new LerpSettings();
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
            var t = settings.Curve.Evaluate(progressPercent);
            var result = Data.Evaluate(t);
            Data.Current = result;
            return result;
        }

        void UpdateProgress(float deltaTime)
        {
            if (IsComplete)
                return;
            
            currentTime += deltaTime;
            if (currentTime > settings.Duration)
            {
                currentTime = settings.Duration;
            }

            progressPercent = currentTime / settings.Duration;
            if (progressPercent >= settings.PercentThreshold)
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

