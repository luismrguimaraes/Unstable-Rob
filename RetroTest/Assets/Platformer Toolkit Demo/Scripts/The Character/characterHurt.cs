using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;


namespace GMTK.PlatformerToolkit {
    //This script handles the character being killed and respawning

    public class characterHurt : MonoBehaviour {
        [Header("Components")]
        [SerializeField] Vector3 checkpointFlag;
        [SerializeField] Animator myAnim;
        [SerializeField] AudioSource hurtSFX;
        private Coroutine flashRoutine;
        Rigidbody2D body;
        [SerializeField] public SpriteRenderer spriteRenderer;
        [SerializeField] movementLimiter myLimit;


        [Header("Settings")]
        [SerializeField] float respawnTime;
        [SerializeField] private float flashDuration;

        [Header("Events")]
        [SerializeField] public UnityEvent onHurt = new UnityEvent();

        [Header("Current State")]
        bool waiting = false;
        bool hurting = false;

        void Start() {
            body = GetComponent<Rigidbody2D>();
        }

        public void newCheckpoint(Vector3 flagPos) {
            //When the player touches a checkpoint, it passes its position to this script
            checkpointFlag = flagPos;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            //If the player hits layer 7 (saw blade) or 8 (spikes), start the hurt routine
            if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8) {
                if (hurting == false) {
                    //If it's spikes, stop the character's velocity
                    if (collision.gameObject.layer == 8) {
                        body.velocity = Vector2.zero;
                    }

                    hurting = true;
                    hurtRoutine();
                }
            }
        }


        public void hurtRoutine() {
            myLimit.CharacterCanMove = false;

            //The screenshake is played in a Unity Event, provided the option is turned on
            onHurt?.Invoke();

            hurtSFX.Play();

            Stop(0.1f);
            myAnim.SetTrigger("Hurt");
            Flash();

            //Start a timer, before respawning the player. This uses the (excellent) free Unity asset DOTween
            float timer = 0;
            DOTween.To(() => timer, x => timer = x, 1, respawnTime).OnComplete(respawnRoutine);
        }

        //These three functions handle the hit stop effect, where the game pauses for a brief moment on death
        public void Stop(float duration) {
            Stop(duration, 0.0f);
        }

        public void Stop(float duration, float timeScale) {
            if (waiting)
                return;
            Time.timeScale = timeScale;
            StartCoroutine(Wait(duration));
        }

        IEnumerator Wait(float duration) {
            waiting = true;
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1.0f;
            waiting = false;
        }

        //These two functions handle the flashing white effect when Kit dies
        public void Flash() {
            if (flashRoutine != null) {
                StopCoroutine(flashRoutine);
            }

            flashRoutine = StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine() {
            // Show the flash
            spriteRenderer.enabled = true;

            // Pause the execution of this function for "duration" seconds.
            yield return new WaitForSeconds(flashDuration);

            // Hide the flash
            spriteRenderer.enabled = false;

            // Set the routine to null, signaling that it's finished.
            flashRoutine = null;
        }

        //After the timer ends, respawn Kit at the nearest checkpoint and let her move again
        private void respawnRoutine() {
            transform.position = checkpointFlag;
            myLimit.CharacterCanMove = true;
            myAnim.SetTrigger("Okay");
            hurting = false;
        }
    }
}