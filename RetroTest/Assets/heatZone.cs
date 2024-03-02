using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatZone : MonoBehaviour
{

    // Script that when colliding the with "Player" object, will increase (or slow down) the heat velocity of the player
    // By default assumes heat, but can be set to cold

    public LayerMask Player; // The layer that the player is on
    public Collider2D triggerCollider;
    public bool coldZone = false;



    // Start is called before the first frame update
    void Start()
    {

        // Set color to red, if it's a heat zone, and blue if it's a cold zone
        if (coldZone){
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.5f);
        } else {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
        }
        
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
            if (coldZone){
                heatUI.GetComponent<HeatControl>().ColdZone();
            } else {
                heatUI.GetComponent<HeatControl>().HeatZone();
            }
        }
        
    }
}
