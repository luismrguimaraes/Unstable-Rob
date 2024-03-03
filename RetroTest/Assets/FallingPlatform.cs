using System.Collections;
using System.Collections.Generic;
using FMODUnityResonance;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallTime = 1;
    public FMODUnity.EventReference fallSfx;
    public bool playSfx;
    private float ShakeAcc = 0;
    private float YVol = 0;
    private Rigidbody2D rb;
    private BoxCollider2D collider;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallActivator"))
            InitFall();
    }

    public void InitFall()
    {
        Invoke("Shake", 0.05f);
    }

    private void Shake()
    {
        Vector2 offset = new Vector2(0, Random.Range(-0.1f, 0.05f));
        collider.offset -= offset;
        transform.position += new Vector3(0,offset.y,0);

        float nextshake = fallTime / 6 - ShakeAcc / 6;
        ShakeAcc += nextshake;

        if (ShakeAcc+0.001f >= fallTime){
            // Start Falling
            InvokeRepeating("Fall", nextshake, 0.01f);

            // Sfx
            if (playSfx)
                FMODUnity.RuntimeManager.CreateInstance(fallSfx).start();
        }
        else
            Invoke("Shake", nextshake);
    }

    private void Fall()
    {
        collider.enabled = false;
        if (YVol > 2)
            Die();
        YVol += 0.01f;
        transform.position += new Vector3(0, -YVol, 0);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
