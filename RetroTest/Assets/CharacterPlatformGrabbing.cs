using GMTK.PlatformerToolkit;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPlatformGrabbing : MonoBehaviour
{
    public InputActionAsset controls;
    public InputAction grabAction;
    public characterJump charJump;
    public float y_offset = -0.2f;
    public bool isGrabbing;
    private bool touchingPlatform;
    public float grabCoyote = 0.5f;
    public float grabTimer = 0f;
    public float resetTimer = 0f;



    private void Start(){
        grabAction = controls.FindAction("Grab");
        print(grabAction);
    }
    private void Update()
    {
        if (!isGrabbing)
        {
            grabTimer += Time.deltaTime;
            resetTimer += Time.deltaTime;
        }
        else
        {
            grabTimer = 0;
            resetTimer = 0;
        }
        if (grabAction.IsPressed() && !isGrabbing && grabResetElapsed())
        {
            print("canGrab");
            print(touchingPlatform);
            print("!On Ground");
            print(!charJump.onGround);

            if (touchingPlatform){
                isGrabbing = true;
            }
        }else{
            if (grabAction.WasReleasedThisFrame()){
                isGrabbing = false;
            }
        }
        return;
    }

    public bool grabCoyoteElapsed()
    {
        return grabTimer >= grabCoyote;
    }

    public bool grabResetElapsed()
    {
        return resetTimer >= grabCoyote;
    }

    private void OnCollisionEnter2D(Collision2D c){
        touchingPlatform = true;
    }

    private void OnCollisionExit2D(Collision2D c){
        touchingPlatform = false;
    }

}
