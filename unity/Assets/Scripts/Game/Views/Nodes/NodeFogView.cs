using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Common.Utils;

namespace syscrawl.Game.Views.Nodes
{
    public class NodeFogView : View
    {
        NodeFogViewSettings settings;

        AnimationCurve lerpCurve;
        float lerpTime = 0f;
        float currentLerpTime = 0f;

        bool isAnimating = false;
        float shaderAlphaCutoutTo = 0f;
        float shaderAlphaCutoutFrom = 1f;


        Renderer sphereRenderer;

        float CurrentShaderAlpha
        { 
            get
            { 
                return sphereRenderer.material.GetFloat("_Cutoff");
            } 
        }

        void OnMouseEnter()
        {
            HideSphere();
        }

        void OnMouseExit()
        {
            ShowSphere();
        }

        void ShowSphere()
        {
            shaderAlphaCutoutTo = 0;
            lerpTime = settings.AppearTime;
            lerpCurve = settings.AppearCurve;
            InitializeSphereAnimation();

        }

        void HideSphere()
        {
            shaderAlphaCutoutTo = 1;
            lerpTime = settings.RevealTime;
            lerpCurve = settings.RevealCurve;
            InitializeSphereAnimation();

        }

        void InitializeSphereAnimation()
        {
            shaderAlphaCutoutFrom = CurrentShaderAlpha;
            currentLerpTime = 0f;
            isAnimating = true;
        }

        void AnimateSphere()
        {
            if (sphereRenderer == null || !isAnimating)
                return;

            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
                currentLerpTime = lerpTime;

            var percentage = currentLerpTime / lerpTime;
            if (percentage >= 0.99)
            {
                percentage = 1f;
                isAnimating = false;
            }

            var value = 
                Mathf.Lerp(
                    shaderAlphaCutoutFrom, shaderAlphaCutoutTo, 
                    lerpCurve.Evaluate(percentage));

            sphereRenderer.material.SetFloat("_Cutoff", value);
        }

        void Update()
        {
            if (!isInitialized)
                return;
            AnimateSphere();
        }

        GameObject fog;
        bool isInitialized = false;

        internal void Init()
        {
            fog = Prefabs.Instantiate("NodeFog", gameObject);
            settings = fog.GetComponent<NodeFogViewSettings>();
            fog.transform.localScale = new Vector3(10, 10, 10);
            fog.GetComponent<Collider>().enabled = false;

            //TODO: Handle the visiblity.
            sphereRenderer = fog.GetComponent<Renderer>();
            sphereRenderer.enabled = false;

            isInitialized = true;
        }
    }
}

