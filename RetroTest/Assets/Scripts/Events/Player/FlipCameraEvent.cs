using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCameraEvent : Event {
    public FlipCameraEvent()
    {
        Id = "FlipCamera";
    }
    
    public override void Initialize()
    {
        data.virtualCamera.m_Lens.Dutch = 180;
        data.movementDataController._moveScript.invertControlsEvent();
        data.UIcamera.transform.localRotation = Quaternion.Euler(0, 0, 180);
    }

    public override void End()
    {
        data.virtualCamera.m_Lens.Dutch = 0;
        data.movementDataController._moveScript.invertControlsEvent();
        data.UIcamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}