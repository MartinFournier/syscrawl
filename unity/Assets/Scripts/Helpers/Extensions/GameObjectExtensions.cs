using UnityEngine;

namespace syscrawl.Extensions
{
    public static class GameObjectExtensions
    {
        public static GameObject CreateSubcomponent<T>(
            this GameObject parentObject, string name = null) 
            where T: MonoBehaviour
        {
            GameObject go = new GameObject();
            go.name = name;
            go.AddComponent<T>();
            go.transform.parent = parentObject.transform;
            return go;
        }
    }
}

