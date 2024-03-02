using GMTK.PlatformerToolkit;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPlatformGrabbing : MonoBehaviour
{
    public InputActionAsset controls;
    public InputAction grabAction;
    public characterJump charJump;
    public GameObject grabPlatform;
    private GameObject instanciatedGrabPlatform;
    public float y_offset = -0.2f;
    public bool isGrabbing;
    private bool canGrabPlatform;


    private void Start(){
        grabAction = controls.FindAction("Grab");
        print(grabAction);
    }
    private void Update()
    {
        if (grabAction.IsPressed() && !isGrabbing)
        {
            print("canGrab");
            print(canGrabPlatform);
            print("!On Ground");
            print(!charJump.onGround);

            if (canGrabPlatform){
                isGrabbing = true;
                instanciatedGrabPlatform = Instantiate(grabPlatform, new Vector3 (charJump.gameObject.transform.position.x, gameObject.transform.position.y + y_offset, gameObject.transform.position.z) , Quaternion.identity);
            }
        }else{
            if (grabAction.WasReleasedThisFrame()){
                if (instanciatedGrabPlatform) Destroy(instanciatedGrabPlatform);
                isGrabbing = false;
            }
        }
        return;
    }

    private void OnCollisionEnter2D(Collision2D c){
        if (!charJump.onGround){
            canGrabPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D c){
        canGrabPlatform = false;
        if (isGrabbing){
            Destroy(instanciatedGrabPlatform);
            isGrabbing = false;
        }
    }

}
