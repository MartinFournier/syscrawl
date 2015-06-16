using System;

namespace syscrawl.Common.Utils.Lerp
{
    public static class LerpExtensions
    {
        public static bool IsUpdating<T>(this Lerp<T> lerp)
        {
            return lerp != null && !lerp.IsComplete;
        }
            
    }
}

