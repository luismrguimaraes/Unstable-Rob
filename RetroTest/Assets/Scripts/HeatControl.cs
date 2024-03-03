using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeatControl : MonoBehaviour
{

    [SerializeField] public float HeatLevel;
    [SerializeField] private Image image;
    [SerializeField] private float BaseHeatSpeed;
    [SerializeField] private float HeatSpeed;
    [SerializeField] private GameObject playerChar;
    private Vector3 originalPosition;
    public GameObject eventPrefab;
    private LinkedList<PlayerEventBlock> blocks = new LinkedList<PlayerEventBlock>();
    public float[] limits;
    private int nextLimit = 0;
    [SerializeField] private Transform bar;
    [SerializeField] private float coolingSpeed = 0.5f;
    private bool followingPlayer;

    private void Start()
    {
        originalPosition = bar.localPosition;
    }

    void Update()
    {
        HeatSpeed = BaseHeatSpeed / (FindCurrentLimit() + 1);
        HeatLevel += Time.deltaTime*HeatSpeed;
        HeatLevel = Mathf.Clamp(HeatLevel, 0, 1);
        image.fillAmount = HeatLevel;
        // Update FMOD parameter
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("HeatLevel", HeatLevel);
        
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
            nextLimit++;
            GameObject obj = Instantiate(eventPrefab, gameObject.transform);
            obj.transform.localPosition += new Vector3(90, 0, 0);
            blocks.AddLast(obj.GetComponent<PlayerEventBlock>());
            blocks.Last.Value.targetPos = blocks.Last.Value.transform.localPosition;
            foreach (PlayerEventBlock block in blocks)
                block.targetPos += new Vector3(0, -100, 0);

            FollowPlayerFirst(obj);

        } else if(nextLimit >= 1 && HeatLevel < limits[nextLimit-1])
        {
            blocks.First.Value.Remove();
            blocks.RemoveFirst();
            nextLimit--;
        }

        if (followingPlayer){
            blocks.Last.Value.targetPos = playerChar.transform.position - new Vector3 (0, +25f, 0f);
        }
    }

    public void FollowPlayerFirst(GameObject obj)
    {
        followingPlayer = true;
        Vector3 previousPosition = blocks.Last.Value.targetPos;

        blocks.Last.Value.targetPos = playerChar.transform.position;
        StartCoroutine(FollowPlayerCoroutine(previousPosition));
    }

    IEnumerator FollowPlayerCoroutine(Vector3 previousPosition){
        yield return new WaitForSeconds(2f + 2f);//blocks.Last.Value.gameObject.GetComponent<PlayerEventBlock>().eventEffectDelay);

        followingPlayer = false;
        blocks.Last.Value.targetPos = previousPosition;
    }

    // Event that cools the player down a bit
    public void WaterCooling()
    {
        HeatLevel -= 0.1f*coolingSpeed*HeatSpeed;
        HeatLevel = Mathf.Clamp(HeatLevel, 0, 1);
    }

    private int FindCurrentLimit()
    {
        for(int i = 0; i < limits.Length; i++)
        {
            if(HeatLevel < limits[i])
                return i;
        }
        return limits.Length;
    }

    // Event that accelerates the heat speed of the player
    public void HeatZone()
    {
        HeatLevel += 0.02f*HeatSpeed;
        HeatLevel = Mathf.Clamp(HeatLevel, 0, 1);
        Debug.Log("HeatZone");
    }

    // Event that slows down the heat speed of the player
    public void ColdZone()
    {
        HeatLevel -= 0.007f*HeatSpeed;
        HeatLevel = Mathf.Clamp(HeatLevel, 0, 1);
        Debug.Log("ColdZone");
    }
    


}
