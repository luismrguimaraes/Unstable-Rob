using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class OneWayMovingPlatform : MonoBehaviour
{
    public bool active = false;
    public float speed;
    public Vector3 targetOffset;
    private Transform previousPlayerParent;
    private Vector3 origin;
    private bool done = false;


    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!active)
            return;
        if ((transform.position - (origin + targetOffset)).magnitude > 0.05f)
            transform.position += targetOffset.normalized * Time.deltaTime * speed;
        else if (!done) {
            done = true;
            gameObject.AddComponent<FallingPlatform>();
        }

    }

    void OnCollisionEnter2D(Collision2D c){
        print(c);
        if (!c.gameObject.CompareTag("Player"))
            return;
        active = true;
        previousPlayerParent = c.gameObject.transform.parent.parent;
        c.gameObject.transform.parent.parent = transform;

    }
    void OnCollisionExit2D(Collision2D c){
        if (!c.gameObject.CompareTag("Player"))
            return;
        c.gameObject.transform.parent.parent = previousPlayerParent;

    }
}
