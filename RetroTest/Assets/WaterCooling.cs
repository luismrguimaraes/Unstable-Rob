using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCooling : MonoBehaviour
{

    // Script that when colliding the with "Player" object, will access the HeatUI object
    public LayerMask Player; // The layer that the player is on
    public Collider2D triggerCollider;

    // waterRemaining
    public double waterRemaining;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            
        if (triggerCollider.IsTouchingLayers(Player)){
            // Find the player object
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Get the object with tag HeatUI
            GameObject heatUI = GameObject.FindGameObjectWithTag("HeatUI");

            // Call the WaterCooling function from the HeatUI script
            heatUI.GetComponent<HeatControl>().WaterCooling();

            // reduce the cooling time a bit
            waterRemaining -= 0.5*Time.deltaTime;

            // Print remaining cooling time
            Debug.Log("Water Remaining: " + waterRemaining);


        }

        if (waterRemaining <= 0)
        {
            Destroy(gameObject);
        }

        // Floating animation
        //transform.position += new Vector3(0, Mathf.Sin(Time.time * 3) * 0.01f, 0);
        
    }
}
