using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEventBlock : MonoBehaviour
{
    public Sprite[] eventSprites;
    public string[] events;
    private EvenTakenSingleton eventsTaken;
    private float timeAlive;
    private float accTime;
    private int curDisplay;
    private Image img;
    private bool locked = false;
    private EventsController eventsControl;
    public Vector3 targetPos;

    private void Start()
    {
        img = GetComponent<Image>();
        eventsControl = FindFirstObjectByType<EventsController>();
        eventsTaken = FindFirstObjectByType<EvenTakenSingleton>();
        curDisplay = Random.Range(0, events.Length);
    }

    private void Update()
    {
        Vector3 move = (targetPos - transform.localPosition);

        if(move.magnitude > 0.05f)
        {
            transform.localPosition += move * 5 * Time.deltaTime;
        }
        if (locked)
            return;
        timeAlive += Time.deltaTime;
        if(timeAlive > 2)
        {
            locked = true;
            eventsControl.AddEventByName(events[curDisplay]);
            eventsTaken.list.Add(curDisplay);
        } else
        {
            accTime += Time.deltaTime;
            bool fastTime = accTime > 0.1 && timeAlive < 1;
            bool medTime = accTime > 0.2 && timeAlive < 1.5;
            bool slowTime = accTime > 0.4 && timeAlive < 2;
            if(fastTime || medTime || slowTime)
            {
                accTime = 0;
                do
                {
                    curDisplay++;
                    if (curDisplay >= events.Length)
                        curDisplay = 0;
                } while (eventsTaken.list.Contains(curDisplay));
                img.sprite = eventSprites[curDisplay];
            }
        }
    }
    public void Remove()
    {
        eventsTaken.list.Remove(curDisplay);
        eventsControl.RemoveEventByName(events[curDisplay]);
        targetPos += new Vector3(-100, 0, 0);
        Invoke("Die", 0.5f);
    }

    private void Die()
    {
        Destroy(gameObject);
    }





}
