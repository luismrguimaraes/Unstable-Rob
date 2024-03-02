using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProperties : MonoBehaviour
{
    public FMODUnity.EventReference deathSfx;
    private bool dying = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!dying && transform.position.y < -10)
        {
            Die();
        }
    }

    public void Die()
    {
        dying = true;
        FMODUnity.RuntimeManager.CreateInstance(deathSfx).start();
        gameObject.SetActive(false);
        Invoke(nameof(Restart), 0.33f);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
