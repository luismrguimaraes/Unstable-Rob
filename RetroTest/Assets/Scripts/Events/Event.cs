using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using GMTK.PlatformerToolkit;
using Kino;
using UnityEngine;

[System.Serializable]
public class EventData {
    public Camera camera;
    public CinemachineVirtualCamera virtualCamera;
    public DigitalGlitch digitalGlitch;
    public AnalogGlitch analogGlitch;
    public GameObject player;
    public CharacterMovementDataController movementDataController;
    public characterJump jumpScript;
    public GameObject darkness;
}
public abstract class Event {
    public string Id { get; set; }
    public EventData data;
    
    public virtual void Initialize() {}
    public virtual void Execute() {}
    public virtual void End() {}
}
