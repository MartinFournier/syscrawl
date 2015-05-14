using UnityEngine;

namespace syscrawl.Utils.Lerp
{
    public class VectorLerp : Lerp<Vector3>
    {
        public VectorLerp()
            : base()
        {
            Data = new VectorLerpData();
        }
    }
}

