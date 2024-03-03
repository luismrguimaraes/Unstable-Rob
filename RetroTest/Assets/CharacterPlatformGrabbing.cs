using GMTK.PlatformerToolkit;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPlatformGrabbing : MonoBehaviour
{
    public InputActionAsset controls;
    public InputAction grabAction;
    public InputAction jumpAction;
    public characterJump charJump;
    public FMODUnity.EventReference grabSFX;
    public float y_offset = -0.2f;
    public bool isGrabbing;
    private int touchingPlatforms;
    public float grabCoyote = 0.5f;
    public float grabTimer = 0f;
    public float resetTimer = 0f;

    private void Start(){
        grabAction = controls.FindAction("Grab");
        jumpAction = controls.FindAction("Jump");
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
            if (touchingPlatforms>0){
                isGrabbing = true;

                // Play SFX
                FMODUnity.RuntimeManager.CreateInstance(grabSFX).start();
            }
        }else{
            if (grabAction.WasReleasedThisFrame() || jumpAction.WasPressedThisFrame()){
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
        touchingPlatforms++;
    }

    private void OnCollisionExit2D(Collision2D c){
        touchingPlatforms--;
        if(touchingPlatforms<=0)
        {
            touchingPlatforms = 0;
            isGrabbing = false;
        }
    }

}
