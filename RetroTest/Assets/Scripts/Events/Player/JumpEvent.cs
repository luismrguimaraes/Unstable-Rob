using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEvent : Event
{
    private float timeToJump;
    private float minTimeToJump = 0.5f;
    private float maxTimeToJump = 1.5f;
    private float jumpForce = 10f;
    private float adjustRange = 1.7f;
    
    public JumpEvent()
    {
        Id = "Jump";
    }

    public override void Execute()
    {
        if (data.jumpScript.onGround)
        {
            if (timeToJump > 0)
            {
                timeToJump -= Time.deltaTime;
            }
            else
            {
                data.jumpScript.bounce(generateRandomJumpForce());
                setTimeToJump();
            }
        }
    }
    
    public override void End()
    {
        timeToJump = 0;
    }
    
    private void setTimeToJump()
    {
        timeToJump = Random.Range(minTimeToJump, maxTimeToJump);
    }
    
    private Vector2 generateRandomJumpForce()
    {
        float x = Random.Range(-adjustRange, adjustRange);
        x = Mathf.Clamp(x, -1, 1);
        x *= jumpForce;
        return new Vector2(x, jumpForce);
    }
}