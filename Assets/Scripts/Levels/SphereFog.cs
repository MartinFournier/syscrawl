using UnityEngine;
using System.Collections;

public class SphereFog : MonoBehaviour
{

    public AnimationCurve RevealCurve;
    public AnimationCurve AppearCurve;
    public float RevealTime = 1f;
    public float AppearTime = 1f;

    AnimationCurve lerpCurve;
    float lerpTime = 0f;
    float currentLerpTime = 0f;

    Renderer sphereRenderer;

    bool isAnimating = false;
    float shaderAlphaCutoutTo = 0f;
    float shaderAlphaCutoutFrom = 1f;

    void Start()
    {
        sphereRenderer = GetComponent<Renderer>();
    }

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
        lerpTime = AppearTime;
        lerpCurve = AppearCurve;
        InitializeSphereAnimation();

    }

    void HideSphere()
    {
        shaderAlphaCutoutTo = 1;
        lerpTime = RevealTime;
        lerpCurve = RevealCurve;
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
        AnimateSphere();
    }
}
