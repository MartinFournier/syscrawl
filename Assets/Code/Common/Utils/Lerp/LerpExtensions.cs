namespace syscrawl.Common.Utils.Lerp
{
    public static class LerpExtensions
    {
        public static bool IsUpdating<T>(this Lerp<T> lerp)
        {
            return lerp != null && !lerp.IsComplete;
        }

        public static void Update<T>(this Lerp<T> lerp, float deltaTime)
        {
            if (!lerp.IsUpdating())
                return;
            
            lerp.Evaluate(deltaTime);
        }
    }
}

