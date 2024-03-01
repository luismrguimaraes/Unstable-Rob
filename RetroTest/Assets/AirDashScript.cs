using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDashScript : MonoBehaviour


// Script that controls each AirDash Coin.
// When the character colides with coin, the character's upwards velocity is increased by 5




{

    public LayerMask Player; // The layer that the player is on
    public Collider2D triggerCollider;

    // This object, which will be used to check if the player is touching the coin


    public void Update()
    {

        if (triggerCollider.IsTouchingLayers(Player)){
            // Find the player object
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("Player has collided with AirDash Coin");
            Debug.Log("Player Object: " + player);
        }

        // Floating animation
        transform.position += new Vector3(0, Mathf.Sin(Time.time * 3) * 0.01f, 0);

    }




}
