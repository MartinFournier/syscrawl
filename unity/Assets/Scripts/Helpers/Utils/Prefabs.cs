using System;
using UnityEngine;

namespace syscrawl.Utils
{
    public static class Prefabs
    {
        const string format = "Prefabs/{0}";

        static string GetPrefabKey(string name)
        {
            return String.Format(format, name);
        }

        public static GameObject Instantiate(string name)
        {
            
            var resourceKey = GetPrefabKey(name);
            var resource = Resources.Load(resourceKey);
            var prefab = UnityEngine.Object.Instantiate(resource);
            return prefab as GameObject;
        }
     
    }
}

