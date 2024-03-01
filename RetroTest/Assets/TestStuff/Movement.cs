using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    public FMODUnity.EventReference jumpSfx;
    public Camera cam;

    Controls controls;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        controls = new Controls();
        controls.asset.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int dir = 0;
        if (controls.Move.Right.IsPressed()) dir += 1;
        if (controls.Move.Left.IsPressed()) dir -= 1;

        rb.AddForce(new Vector2(dir, 0));

        if (controls.Jump.Jump.WasPressedThisFrame()) {
            rb.AddForce(new Vector2(0, 250));
            // play sound
            FMODUnity.RuntimeManager.CreateInstance(jumpSfx).start();
        }
    }
}
