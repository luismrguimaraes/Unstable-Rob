using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public bool inMusicZone;

    // Start is called before the first frame update
    void Start()
    {
        inMusicZone = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D (Collider2D c){
        if (c.gameObject.tag == "MusicZone"){
            inMusicZone = true;
        }
    }
    void OnTriggerExit2D (Collider2D c){
        if (c.gameObject.tag == "MusicZone"){
            inMusicZone = false;
        }
    }
}
