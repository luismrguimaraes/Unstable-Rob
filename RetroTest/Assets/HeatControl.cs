using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatControl : MonoBehaviour
{

    [SerializeField] private float HeatLevel;
    [SerializeField] private Image image;
    [SerializeField] private float HeatSpeed;
    public Gradient gradient;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        HeatLevel += Time.deltaTime*HeatSpeed;
        HeatLevel = Mathf.Clamp(HeatLevel, 0, 1);
        image.fillAmount = HeatLevel;
        image.color = gradient.Evaluate(HeatLevel);
        if(HeatLevel > 0.5f)
        {
            float shakeAmount = (HeatLevel - 0.5f);
            float xOffset = Mathf.PerlinNoise(Time.time * 6, 0);
            float yOffset = Mathf.PerlinNoise(0, Time.time * 6);
            Debug.Log(xOffset);
            Debug.Log(yOffset);
            transform.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0) * shakeAmount;
        }
    }


}
