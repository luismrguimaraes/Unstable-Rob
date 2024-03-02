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
    public GameObject eventPrefab;
    private LinkedList<PlayerEventBlock> blocks = new LinkedList<PlayerEventBlock>();
    public float[] limits;
    private int nextLimit = 0;
    [SerializeField] private Transform bar;

    private void Start()
    {
        originalPosition = bar.localPosition;

        //Time.timeScale = 0.1f;
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
            //Debug.Log(xOffset);
            //Debug.Log(yOffset);
            bar.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0) * shakeAmount * 10;
        }

        //handle spawning events
        if(nextLimit < limits.Length && HeatLevel >= limits[nextLimit])
        {
            var obj = Instantiate(eventPrefab, gameObject.transform);
            obj.transform.localPosition += new Vector3(90, 0, 0);
            blocks.AddLast(obj.GetComponent<PlayerEventBlock>());
            blocks.Last.Value.targetPos = blocks.Last.Value.transform.localPosition;
            nextLimit++;
            foreach (PlayerEventBlock block in blocks)
                block.targetPos += new Vector3(0, -60, 0);
        } else if(nextLimit >= 1 && HeatLevel < limits[nextLimit-1])
        {
            blocks.First.Value.Remove();
            blocks.RemoveFirst();
            nextLimit--;
        }
    }


}
