using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    private void Start()
    {
        Invoke("MainMen", 3f);
    }

    private void MainMen()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
