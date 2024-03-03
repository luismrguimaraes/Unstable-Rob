using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCooling : MonoBehaviour
{

    // Script that when colliding the with "Player" object, will access the HeatUI object
    public LayerMask Player; // The layer that the player is on
    public Collider2D triggerCollider;

    // water 
    public double waterPercentage = 50;

    // List of sprites for the water tank
    public Sprite[] waterTankSprites;

    // infiniteWater boolean
    public bool infiniteWater;

    // Start is called before the first frame update
    void Start()
    {
        
        if (waterPercentage > 0)
        {
            RefreshSprite();
        }
        
    }

    // Update is called once per frame
    void Update()
    {

            
        if (triggerCollider.IsTouchingLayers(Player) && waterPercentage > 0){
            // Find the player object
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Get the object with tag HeatUI
            GameObject heatUI = GameObject.FindGameObjectWithTag("HeatUI");

            // Call the WaterCooling function from the HeatUI script
            heatUI.GetComponent<HeatControl>().WaterCooling();

            // reduce the cooling time a bit
            if (!infiniteWater){
                waterPercentage -= 10*Time.deltaTime;
            }


        }

        // // Destroy the object if the water is empty
        // if (waterRemaining <= 0 && !infiniteWater)
        // {
        //     Destroy(gameObject);
        // }

        // Refresh sprite
        if (waterPercentage > 0)
        {
            RefreshSprite();
        }

        // Floating animation
        //transform.position += new Vector3(0, Mathf.Sin(Time.time * 3) * 0.01f, 0);
        
    }

    // refresh sprite
    public void RefreshSprite()
    {
        
        if (infiniteWater)
        {
            GetComponent<SpriteRenderer>().sprite = waterTankSprites[19];
        }

        else{
        // Checks the water level, and applies the correct sprite (WaterTank_X) where X is from 0 (full) to 18 (empty), uniformly distributed
        int spriteIndex = (int) Mathf.Floor((float)waterPercentage/100*18);

        // Load the sprite WaterTank_X from the array
        Debug.Log("Sprite Index: " + spriteIndex);
        try
        {
            GetComponent<SpriteRenderer>().sprite = waterTankSprites[spriteIndex];
        }
        catch (System.Exception)
        {
            Debug.Log("Error: Sprite index out of range (" + spriteIndex + ").");
        }
        }
    }
}
