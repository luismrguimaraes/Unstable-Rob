using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using FMODUnityResonance;
using GMTK.PlatformerToolkit;
using UnityEngine;

public class AirDashScript : MonoBehaviour
// Script that controls each AirDash Coin.
// When the character colides with coin, the character's upwards velocity is increased by 5
{
    public CinemachineVirtualCamera mainCamera; // The main camera
    public characterMovement charMove; // The character movement script
    public LayerMask Player; // The layer that the player is on
    public Collider2D triggerCollider;

    // respawnTime and cooldown variables (seconds)
    public float respawnTime = 3;
    public float cooldown = 0;
    public float targetOrtoSize = 13.5f;
    

    // force multipler variable
    public float forceMultiplier = 1f;

    // SFX
    public FMODUnity.EventReference airDashSfx;

    public void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (triggerCollider.IsTouchingLayers(Player) && cooldown <= 0){

            // calling the airdash function from the player's script
            charMove.AirDash(forceMultiplier, airDashSfx);
            StartCoroutine(AirDashImpulse());

            // Disable rendering only (not the object itself)
            GetComponent<SpriteRenderer>().enabled = false;
            cooldown = respawnTime*2;
        }

        // Floating animation
        transform.position += new Vector3(0, Mathf.Sin(Time.time * 3) * 0.01f, 0);
        

    }

    private IEnumerator AirDashImpulse()
    {
        float originalSize = mainCamera.m_Lens.OrthographicSize;
        float duration = 0.35f;
        // Zoom out the camera in fade
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            mainCamera.m_Lens.OrthographicSize = Mathf.Lerp(originalSize, targetOrtoSize, t/duration);
            yield return null;
        }
        
        // Wait for charMove to be on ground
        while (!charMove.onGround)
        {
            yield return null;
        }
        
        // Zoom in the camera in fade
        t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            mainCamera.m_Lens.OrthographicSize = Mathf.Lerp(targetOrtoSize, originalSize, t/duration);
            yield return null;
        }
    }




}
