using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KILLOnTouch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponent<PlayerProperties>().Die();
        }
    }
}
