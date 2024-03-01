using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertMovementEvent : Event {
    public override void Initialize()
    {
        this.data.movementDataController._moveScript.invertControlsEvent();
    }

    public override void End()
    {
        this.data.movementDataController._moveScript.invertControlsEvent();
    }
}
