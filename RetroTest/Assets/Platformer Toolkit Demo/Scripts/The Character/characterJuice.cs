using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GMTK.PlatformerToolkit {
    //This script handles purely aesthetic things like particles, squash & stretch, and tilt

    public class characterJuice : MonoBehaviour {
        [Header("Components")]
        characterMovement moveScript;
        characterJump jumpScript;
        [SerializeField] Animator myAnimator;
        [SerializeField] GameObject characterSprite;

        [Header("Components - Particles")]
        [SerializeField] private ParticleSystem moveParticles;
        [SerializeField] private ParticleSystem jumpParticles;
        [SerializeField] private ParticleSystem landParticles;

        [Header("Components - Audio")]
        [SerializeField] FMODUnity.EventReference jumpSFX;
        [SerializeField] FMODUnity.EventReference landSFX;

        [Header("Settings - Squash and Stretch")]
        [SerializeField, Tooltip("Width Squeeze, Height Squeeze, Duration")] Vector3 jumpSquashSettings;
        [SerializeField, Tooltip("Width Squeeze, Height Squeeze, Duration")] Vector3 landSquashSettings;
        [SerializeField, Tooltip("How powerful should the effect be?")] public float landSqueezeMultiplier;
        [SerializeField, Tooltip("How powerful should the effect be?")] public float jumpSqueezeMultiplier;
        [SerializeField] float landDrop = 1;

        [Header("Tilting")]
        [SerializeField, Tooltip("How far should the character tilt?")] public float maxTilt;
        [SerializeField, Tooltip("How fast should the character tilt?")] public float tiltSpeed;

        [Header("Calculations")]
        public float runningSpeed;
        public float maxSpeed;

        [Header("Current State")]
        public bool squeezing;
        public bool jumpSqueezing;
        public bool landSqueezing;
        public bool playerGrounded;

        [Header("Platformer Toolkit Stuff")]
        [SerializeField] jumpTester jumpLine;
        public bool cameraFalling = false;

        void Start() {
            moveScript = GetComponent<characterMovement>();
            jumpScript = GetComponent<characterJump>();
        }

        void Update() {
            tiltCharacter();

            //We need to change the character's running animation to suit their current speed
            runningSpeed = Mathf.Clamp(Mathf.Abs(moveScript.velocity.x), 0, maxSpeed);
            myAnimator.SetFloat("runSpeed", runningSpeed);

            checkForLanding();

            checkForGoingPastJumpLine();
        }

        private void tiltCharacter() {
            //See which direction the character is currently running towards, and tilt in that direction
            float directionToTilt = 0;
            if (moveScript.velocity.x != 0) {
                directionToTilt = Mathf.Sign(moveScript.velocity.x);
            }

            //Create a vector that the character will tilt towards
            Vector3 targetRotVector = new Vector3(0, 0, Mathf.Lerp(-maxTilt, maxTilt, Mathf.InverseLerp(-1, 1, directionToTilt)));

            //And then rotate the character in that direction
            myAnimator.transform.rotation = Quaternion.RotateTowards(myAnimator.transform.rotation, Quaternion.Euler(-targetRotVector), tiltSpeed * Time.deltaTime);
        }

        private void checkForLanding() {
            if (!playerGrounded && jumpScript.onGround) {
                //By checking for this, and then immediately setting playerGrounded to true, we only run this code once when the player hits the ground 
                playerGrounded = true;
                cameraFalling = false;

                //This is related to the "ignore jumps" option on the camera panel.
                jumpLine.characterY = transform.position.y;

                //Play an animation, some particles, and a sound effect when the player lands
                myAnimator.SetTrigger("Landed");
                landParticles.Play();

                if (!landSFX.IsNull){
                    FMOD.Studio.EventInstance landSFXInstance = FMODUnity.RuntimeManager.CreateInstance(landSFX);
                    landSFXInstance.start();
                }

                moveParticles.Play();

                //Start the landing squash and stretch coroutine.
                if (!landSqueezing && landSqueezeMultiplier > 1) {
                    StartCoroutine(JumpSqueeze(landSquashSettings.x * landSqueezeMultiplier, landSquashSettings.y / landSqueezeMultiplier, landSquashSettings.z, landDrop, false));
                }

            }
            else if (playerGrounded && !jumpScript.onGround) {
                // Player has left the ground, so stop playing the running particles
                playerGrounded = false;
                moveParticles.Stop();
            }
        }

        private void checkForGoingPastJumpLine() {
            //This is related to the "ignore jumps" option on the camera panel.
            if (transform.position.y < jumpLine.transform.position.y - 3) {
                cameraFalling = true;
            }

            if (cameraFalling) {
                jumpLine.characterY = transform.position.y;
            }
        }

        public void jumpEffects() {
            //Play these effects when the player jumps, courtesy of jump script
            myAnimator.ResetTrigger("Landed");
            myAnimator.SetTrigger("Jump");

            if (!jumpSFX.IsNull){
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IsJumping", 1f); 
                FMOD.Studio.EventInstance jumpSFXInstance = FMODUnity.RuntimeManager.CreateInstance(jumpSFX);
                jumpSFXInstance.start();
                
            }

            if (!jumpSqueezing && jumpSqueezeMultiplier > 1) {
                StartCoroutine(JumpSqueeze(jumpSquashSettings.x / jumpSqueezeMultiplier, jumpSquashSettings.y * jumpSqueezeMultiplier, jumpSquashSettings.z, 0, true));

            }

            jumpParticles.Play();
        }

        IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds, float dropAmount, bool jumpSqueeze) {
            //We log that the player is squashing/stretching, so we don't do these calculations more than once
            if (jumpSqueeze) { jumpSqueezing = true; }
            else { landSqueezing = true; }
            squeezing = true;

            Vector3 originalSize = Vector3.one;
            Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);

            Vector3 originalPosition = Vector3.zero;
            Vector3 newPosition = new Vector3(0, -dropAmount, 0);

            //We very quickly lerp the character's scale and position to their squashed and stretched pose...
            float t = 0f;
            while (t <= 1.0) {
                t += Time.deltaTime / 0.01f;
                characterSprite.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
                characterSprite.transform.localPosition = Vector3.Lerp(originalPosition, newPosition, t);
                yield return null;
            }

            //And then we lerp back to the original scale and position at a speed dicated by the developer
            //It's important to do this to the character's sprite, not the gameobject with a Rigidbody an/or collision detection
            t = 0f;
            while (t <= 1.0) {
                t += Time.deltaTime / seconds;
                characterSprite.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
                characterSprite.transform.localPosition = Vector3.Lerp(newPosition, originalPosition, t);
                yield return null;
            }

            if (jumpSqueeze) { jumpSqueezing = false; }
            else { landSqueezing = false; }

            squeezing = false;
        }
    }
}