using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    public EventData playerEventData;
    public LinkedList<Event> events;
    
    protected void Start()
    {
        events = new LinkedList<Event>();
        AddEvent(new JumpEvent());
    }

    protected void Update()
    {
        for (LinkedListNode<Event> node = events.First; node != null; node = node.Next)
        {
            node.Value.Execute();
        }
    }

    public void AddEvent(Event event_)
    {
        event_.data = playerEventData;
        events.AddLast(event_);
        event_.Initialize();
    }
    
    public void RemoveEvent(Event event_)
    {
        events.Remove(event_);
        event_.End();
    }

    public void RemoveEvent()
    {
        // Removes first event, but first end it
        events.First.Value.End();
        events.RemoveFirst();
    }
    
    public void RemoveAllEvents()
    {
        foreach (Event event_ in events)
        {
            event_.End();
        }
        events.Clear();
    }
}
