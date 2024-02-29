using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


namespace GMTK.PlatformerToolkit {
    public class jumpTester : MonoBehaviour {
        [SerializeField] CinemachineVirtualCamera theCamera;



        public float characterY = -3.89f;
        [SerializeField] Transform characterTransform;

        [SerializeField] characterJump jumpScript;

        [SerializeField] float offset;

        public bool ignoringJump = false;
        public bool roundedBoundingBox = false;

        [SerializeField] BoxCollider2D squareBox;
        [SerializeField] BoxCollider2D roundedBox;

        [SerializeField] GameObject squareBoxGO;
        [SerializeField] GameObject roundedBoxGO;

        [SerializeField] public AudioSource jumpSFX;
        [SerializeField] public AudioSource landSFX;
        public bool jumpSFXisOn;
        public bool landSFXisOn;


        public float runParticles;
        public float jumpParticles;
        public float landParticles;

        public bool shouldShowPresets = false;



        void Start() {

            if (ignoringJump) {
                ignoreCharacterJump();

            }
            else {
                followCharacterJump();
            }

            if (roundedBoundingBox) {
                switchToRoundedBox();
            }
            else {
                switchToSquareBox();
            }



        }
        [ContextMenu("Flip")]
        void flip() {
            if (ignoringJump) {
                ignoringJump = false;
                followCharacterJump();
            }
            else {
                ignoringJump = true;
                ignoreCharacterJump();

            }
        }


        [ContextMenu("Flip Bounding")]
        void flipBounds() {
            if (roundedBoundingBox) {
                roundedBoundingBox = false;
                switchToSquareBox();
            }
            else {
                roundedBoundingBox = true;
                switchToRoundedBox();

            }
        }





        void Update() {
            transform.position = new Vector3(characterTransform.position.x, characterY);
        }

        public void toggleFollowJump(bool turnOn) {
            if (!turnOn) {
                ignoringJump = false;
                followCharacterJump();
            }
            else {
                ignoringJump = true;
                ignoreCharacterJump();

            }
        }

        public void followCharacterJump() {
            theCamera.Follow = characterTransform;
        }

        public void ignoreCharacterJump() {
            theCamera.Follow = transform;
        }

        public void switchToRoundedBox() {
            roundedBox.enabled = true;
            squareBox.enabled = false;
        }

        public void switchToSquareBox() {
            squareBox.enabled = true;
            roundedBox.enabled = false;
        }


        public void toggleBoundingBox(bool turnOn) {
            if (!turnOn) {
                roundedBoundingBox = false;
                switchToSquareBox();
            }
            else {
                roundedBoundingBox = true;
                switchToRoundedBox();
            }
        }


        public void toggleJumpSFX(bool turnOn) {
            if (!turnOn) {
                jumpSFX.enabled = false;
            }
            else {
                jumpSFX.enabled = true;
            }
        }

        public void toggleLandSFX(bool turnOn) {
            if (!turnOn) {
                landSFX.enabled = false;
            }
            else {
                landSFX.enabled = true;
            }
        }
    }
}