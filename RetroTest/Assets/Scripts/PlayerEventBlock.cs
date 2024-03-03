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
    public float eventEffectDelay;
    public Vector3 targetPos;
    private float animationTimeCounter;
    private bool hasBlinked;

    private void Start()
    {
        eventEffectDelay = 2f;

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
        if(timeAlive > 2 + eventEffectDelay)
        {
            locked = true;
            eventsControl.AddEventByName(events[curDisplay]);
            eventsTaken.list.Add(curDisplay);

            animationTimeCounter = 0;
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

        if (timeAlive > 2){
            // blink animation
            animationTimeCounter += Time.deltaTime;
            if (animationTimeCounter >= eventEffectDelay/3){
                if (!hasBlinked){
                    img.enabled = false;
                    hasBlinked = true;
                }
                else if (animationTimeCounter >= (eventEffectDelay/3 *2f)){
                    img.enabled = true;
                }
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
