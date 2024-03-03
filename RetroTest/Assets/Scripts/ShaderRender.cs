using System.Collections;
using System.Collections.Generic;
using Kino;
using UnityEngine;

public class ShaderRender : MonoBehaviour
{
    public bool isUI = false;
    public Material material;
    public Gradient gradient;
    public HeatControl heatControl;
    
    public DigitalGlitch digitalGlitch;
    public AnalogGlitch analogGlitch;
    
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

   private void Update()
   {
       float level = heatControl.HeatLevel;
        Color matColor = gradient.Evaluate(level);
        material.SetColor("_Color", matColor);
        
        if (isUI) return;
        
        level = Mathf.Pow(heatControl.HeatLevel, 3);
        digitalGlitch.intensity = Mathf.Lerp(0, 0.04f, level);
        
        level = Mathf.Pow(heatControl.HeatLevel, 2);
        analogGlitch.scanLineJitter = Mathf.Lerp(0, 0.125f, level);
        analogGlitch.verticalJump = Mathf.Lerp(0, 0.055f, level);
        analogGlitch.horizontalShake = Mathf.Lerp(0, 0.025f, level);
    }

}
