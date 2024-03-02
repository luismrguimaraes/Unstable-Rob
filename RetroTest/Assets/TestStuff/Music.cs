using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public PlayerProperties playerProps;
    public FMODUnity.EventReference music;
    FMOD.Studio.EventInstance musicInstance;

    // Start is called before the first frame update
    void Start()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(music);
        musicInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
