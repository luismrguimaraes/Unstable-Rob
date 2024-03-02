using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessEvent : Event {
    public DarknessEvent()
    {
        Id = "Darkness";
    }
    
    public override void Initialize()
    {
        data.darkness.SetActive(true);
    }
    
    public override void End()
    {
        data.darkness.SetActive(false);
    }
}