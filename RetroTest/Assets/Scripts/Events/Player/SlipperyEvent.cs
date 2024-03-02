using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyEvent : Event {
    public SlipperyEvent()
    {
        Id = "Slippery";
    }
    
    public override void Initialize()
    {
        this.data.movementDataController._moveScript.switchDecceleration();
    }

    public override void End()
    {
        this.data.movementDataController._moveScript.switchDecceleration();
    }
}