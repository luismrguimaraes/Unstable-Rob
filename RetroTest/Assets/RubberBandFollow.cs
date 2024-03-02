using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBandFollow : MonoBehaviour
{

    public GameObject following;
    public float maxDist = 14;
    public float speed = 1;

    private void Update()
    {
        float dist = following.transform.position.x - transform.position.x;
        if (dist > maxDist)
        {
            transform.position = new Vector3(following.transform.position.x - maxDist,0, 0);
        } else
        {
            transform.position += new Vector3(speed * dist*2 * Time.deltaTime,0,0);
        }
    }
}
