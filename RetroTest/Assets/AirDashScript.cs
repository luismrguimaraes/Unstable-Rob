using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDashScript : MonoBehaviour


// Script that controls each AirDash Coin.
// When the character colides with coin, the character's upwards velocity is increased by 5




{

    public LayerMask Player; // The layer that the player is on
    public Collider2D triggerCollider;

    // force multipler variable
    public float forceMultiplier = 5f;

    // This object, which will be used to check if the player is touching the coin


    public void Update()
    {
        
        if (triggerCollider.IsTouchingLayers(Player)){
            // Find the player object
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // calling the airdash function from the player's script
            player.GetComponent<GMTK.PlatformerToolkit.characterMovement>().AirDash(forceMultiplier);

            // Destroy the coin
            Destroy(gameObject);
        }

        // Floating animation
        transform.position += new Vector3(0, Mathf.Sin(Time.time * 3) * 0.01f, 0);

    }




}
