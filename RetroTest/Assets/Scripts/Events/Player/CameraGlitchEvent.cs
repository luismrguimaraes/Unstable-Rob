using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGlitchEvent : Event {
    public override void Initialize()
    {
        data.digitalGlitch.enabled = !data.digitalGlitch.enabled;
        data.analogGlitch.enabled = !data.analogGlitch.enabled;
    }
    
    public override void End()
    {
        data.digitalGlitch.enabled = !data.digitalGlitch.enabled;
        data.analogGlitch.enabled = !data.analogGlitch.enabled;
    }
}