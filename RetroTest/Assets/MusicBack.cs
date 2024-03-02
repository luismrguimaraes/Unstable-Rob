using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicBack : MonoBehaviour
{
    public FMODUnity.EventReference music;
    FMOD.Studio.EventInstance musicInstance;

    // Start is called before the first frame update
    void Start()
    {
        // Self destruct if music already exists
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(music);
        musicInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy(){
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
