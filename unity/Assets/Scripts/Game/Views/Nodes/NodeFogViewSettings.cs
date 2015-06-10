using UnityEngine;

namespace syscrawl.Game.Views.Nodes
{
    public class NodeFogViewSettings : MonoBehaviour
    {
        public AnimationCurve RevealCurve;
        public AnimationCurve AppearCurve;
        public float RevealTime = 1f;
        public float AppearTime = 1f;
    }
}
