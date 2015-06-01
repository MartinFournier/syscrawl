using UnityEngine;

namespace syscrawl.Common.Extensions
{
    public static class GameObjectExtensions
    {
        public static GameObject AttachObject(
            this GameObject parentObject, string name)
        {
            var go = new GameObject(name);
            go.transform.parent = parentObject.transform;
            return go;
        }

        public static GameObject AttachSubcomponent<T>(
            this GameObject parentObject, string name = null) 
            where T: MonoBehaviour
        {
            var go = parentObject.AttachObject(name);
            go.AddComponent<T>();
            return go;
        }
    }
}

