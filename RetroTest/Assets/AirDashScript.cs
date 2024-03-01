using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDashScript : MonoBehaviour


// Script that controls each AirDash Coin.
// When the character colides with coin, the character's upwards velocity is increased by 5




{

    public LayerMask Player; // The layer that the player is on
    public Collider2D triggerCollider;

    // respawnTime and cooldown variables (seconds)
    public float respawnTime = 3;
    public float cooldown = 0;
    

    // force multipler variable
    public float forceMultiplier = 1f;


    public void Update()
    {

        
        if (cooldown > 0)
        {
            Debug.Log("Cooldown: " + cooldown);
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (triggerCollider.IsTouchingLayers(Player) && cooldown <= 0){
            // Find the player object
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // calling the airdash function from the player's script
            player.GetComponent<GMTK.PlatformerToolkit.characterMovement>().AirDash(forceMultiplier);

            // Disable rendering only (not the object itself)
            GetComponent<SpriteRenderer>().enabled = false;
            cooldown = respawnTime*2;

        }

        // Floating animation
        transform.position += new Vector3(0, Mathf.Sin(Time.time * 3) * 0.01f, 0);
        

    }




}
