using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool active = false;
    public float rate;
    public float amplitude;
    public float timeCounter;
    public Vector3 pivot;
    private Transform previousPlayerParent;
    // Start is called before the first frame update
    void Start()
    {
        pivot = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!active)
            return;
        timeCounter += Time.deltaTime;
        transform.SetPositionAndRotation(new Vector3 (pivot.x + amplitude * Mathf.Sin(2*Mathf.PI * rate * timeCounter) , pivot.y, pivot.z), transform.rotation);
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
