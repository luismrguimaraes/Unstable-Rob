using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(transform.position.y < -5)
        {
            Die();
        }
    }

    public void Die()
    {
        //diesound
        Invoke(nameof(Restart), 1f);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
