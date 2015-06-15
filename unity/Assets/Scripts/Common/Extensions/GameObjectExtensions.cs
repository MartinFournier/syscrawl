using UnityEngine;
using System.Collections.Generic;

namespace syscrawl.Common.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Creates a new empty GameObject with its transform anchored 
        /// at the parentObject's transform
        /// </summary>
        /// <returns>The child GameObject.</returns>
        /// <param name="parentObject">Parent object to anchor the transform to.</param>
        /// <param name="name">The name for the GameObject.</param>
        /// <param name="localPosition">The localPosition to set the transform to.</param> 
        public static GameObject CreateChildObject(
            this GameObject parentObject, 
            string name,
            Vector3 localPosition)
        {            
            var go = new GameObject(name);
            go.transform.parent = parentObject.transform;
            go.transform.localPosition = localPosition;
            return go;
        }

        /// <summary>
        /// Creates a new empty GameObject and attach a component of the given T type,
        /// anchored at the parent's transform.
        /// </summary>
        /// <returns>The child GameObject (not the component).</returns>
        /// <param name="parentObject">Parent object to anchor the transform to.</param>
        /// <param name="name">The name for the GameObject.</param>
        /// <param name="localPosition">The localPosition to set the transform to.</param> 
        /// <typeparam name="T">A monobehaviour.</typeparam>
        private static GameObject CreateChildComponent<T>(
            this GameObject parentObject, 
            string name, 
            Vector3 localPosition) 
            where T: MonoBehaviour
        {
            var go = parentObject.CreateChildObject(name, localPosition);
            go.AddComponent<T>();
           
            return go;
        }

        /// <summary>
        /// Creates a new empty GameObject and attach a component of the given T type,
        /// anchored at the parent's transform.
        /// </summary>
        /// <returns>The subcomponent</returns>
        /// <param name="parentObject">Parent object to anchor the transform to.</param>
        /// <param name="name">The name for the GameObject.</param>
        /// <param name="localPosition">The localPosition to set the transform to.</param> 
        /// <typeparam name="T">A monobehaviour</typeparam>
        public static T CreateSubcomponent<T>(
            this GameObject parentObject, 
            string name,
            Vector3 localPosition) 
            where T: MonoBehaviour
        {
            var go = 
                parentObject.CreateChildComponent<T>(name, localPosition);
            return go.GetComponent<T>();
        }

        //http://forum.unity3d.com/threads/deleting-all-chidlren-of-an-object.92827/
        public static void DestroyChildren(this GameObject go)
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform tran in go.transform)
            {      
                children.Add(tran.gameObject); 
            }
            children.ForEach(child => GameObject.Destroy(child));  
        }

      
    }
}

