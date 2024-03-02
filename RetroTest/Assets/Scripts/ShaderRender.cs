using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderRender : MonoBehaviour
{
    public Material material;
    public Gradient gradient;
    public HeatControl heatControl;
    
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

   private void Update()
    {
        float level = heatControl.HeatLevel;
        Color matColor = gradient.Evaluate(level);
        material.SetColor("_Color", matColor);
    }

}
