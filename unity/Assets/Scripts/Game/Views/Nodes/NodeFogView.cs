using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Common.Utils;
using syscrawl.Common.Utils.Lerp;

namespace syscrawl.Game.Views.Nodes
{
    public class NodeFogView : View
    {
        bool isInitialized = false;
        NodeFogViewSettings settings;
        GameObject fog;
        Renderer sphereRenderer;
        Lerp<float> materialCutoffLerp;

        float CurrentShaderAlpha
        { 
            get
            { 
                return sphereRenderer.material.GetFloat("_Cutoff");
            } 
        }

        internal void Init()
        {
            fog = Prefabs.Instantiate("NodeFog", gameObject);
            sphereRenderer = fog.GetComponent<Renderer>();
            settings = fog.GetComponent<NodeFogViewSettings>();

            materialCutoffLerp = new FloatLerp(settings.lerpSettings);

            fog.transform.localScale = new Vector3(10, 10, 10);
            fog.GetComponent<Collider>().enabled = false;

            isInitialized = true;
        }

        public void ShowSphere()
        {
            materialCutoffLerp.Activate(
                CurrentShaderAlpha, 0f, 
                x => sphereRenderer.material.SetFloat("_Cutoff", x));
        }

        public void HideSphere()
        {
            materialCutoffLerp.Activate(
                CurrentShaderAlpha, 1f,
                x => sphereRenderer.material.SetFloat("_Cutoff", x));
        }

        void Update()
        {
            if (!isInitialized)
                return;
            
            materialCutoffLerp.Update(Time.deltaTime);
        }
    }
}

