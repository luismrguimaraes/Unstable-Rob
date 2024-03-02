using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    private Event[] allEvents = {
        new DarknessEvent(),
        new FlipCameraEvent(),
        new InvertMovementEvent(),
        new JumpEvent(),
        new SlipperyEvent(),
    };
    public EventData playerEventData;
    public LinkedList<Event> events;
    
    protected void Start()
    {
        events = new LinkedList<Event>();
    }

    protected void Update()
    {
        for (LinkedListNode<Event> node = events.First; node != null; node = node.Next)
        {
            node.Value.Execute();
        }
    }

    public void AddEventByName(String name)
    {
        foreach (Event event_ in allEvents)
        {
            if (event_.Id == name)
            {
                AddEvent(event_);
                return;
            }
        }
        Debug.LogError("Event with name " + name + " not found");
    }

    public void AddEvent(Event event_)
    {
        event_.data = playerEventData;
        events.AddLast(event_);
        event_.Initialize();
    }

    public void RemoveEventByName(String name)
    {
        foreach (Event event_ in events)
        {
            if (event_.Id == name)
            {
                RemoveEvent(event_);
                return;
            }
        }
        Debug.LogError("Event with name " + name + " not found");
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
