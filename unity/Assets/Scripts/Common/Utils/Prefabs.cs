using System;
using UnityEngine;

namespace syscrawl.Common.Utils
{
    public static class Prefabs
    {
        const string format = "Prefabs/{0}";

        static string GetPrefabKey(string name)
        {
            return String.Format(format, name);
        }

        /// <summary>
        /// Instantiates a prefab using its resource name only.
        /// Sets the localPosition to a default Vector3.zero value.
        /// </summary>
        /// <param name="name">The name of the Prefab (excluding folders).</param>
        /// <param name="container">The parent object.</param>
        public static GameObject Instantiate(string name, GameObject container)
        {
            var resourceKey = GetPrefabKey(name);
            var resource = Resources.Load(resourceKey);
            var prefab = UnityEngine.Object.Instantiate(resource);

            var gameObject = prefab as GameObject;
            gameObject.transform.parent = container.transform;
            gameObject.transform.localPosition = Vector3.zero;

            return gameObject;
        }
     
    }
}

