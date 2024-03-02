using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject player;
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
        timeCounter += Time.deltaTime;
        transform.SetPositionAndRotation(new Vector3 (pivot.x + amplitude * Mathf.Sin(2*Mathf.PI * rate * timeCounter) , pivot.y, pivot.z), transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D c){
        print(c);
        previousPlayerParent = player.transform.parent;
        player.transform.parent = transform;
    }
    void OnCollisionExit2D(Collision2D c){
        player.transform.parent = previousPlayerParent;

    }
}
