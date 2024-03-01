using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GMTK.PlatformerToolkit {
    public class juiceUpdater : MonoBehaviour {

        [SerializeField] characterJuice juiceScript;

        [SerializeField] ParticleSystem runParticles;
        [SerializeField] ParticleSystem jumpParticles;
        [SerializeField] ParticleSystem landParticles;

        [SerializeField] TrailRenderer characterTrail;

        [SerializeField] jumpTester settingsHome;
        [SerializeField] JuiceSettingsReferences juiceSettingsReferences;


        [Header("Values")]
        [Range(0, 10)]
        public float runParticlesValue = 0;
        [Range(0, 20)]
        public float jumpParticlesValue = 0;
        [Range(0, 50)]
        public float landParticlesValue = 0;
        [Range(0, 1.8f)]
        public float jumpSquashValue = 0;
        [Range(0, 1.8f)]
        public float landSquashValue = 0;
        [Range(0, 10)]
        public float trailValue = 0;
        [Range(-20, 20)]
        public float tiltAngleValue = 0;
        [Range(0, 60)]
        public float tiltSpeedValue = 0;
        public bool jumpSFXToggleValue = false;
        public bool landSFXToggleValue = false;
        public bool updatingInRealtime = false;

        void Update() {
            if (updatingInRealtime) OnEnable();

            if (juiceSettingsReferences) {
                juiceSettingsReferences.runParticleText.text = Mathf.Round(settingsHome.runParticles).ToString();
                juiceSettingsReferences.jumpParticleText.text = Mathf.Round(settingsHome.jumpParticles).ToString();
                juiceSettingsReferences.landParticleText.text = Mathf.Round(settingsHome.landParticles).ToString();

                if (juiceScript.jumpSqueezeMultiplier < 1) {
                    juiceSettingsReferences.jumpSquashText.text = "0";
                }
                else {
                    juiceSettingsReferences.jumpSquashText.text = System.Math.Round(juiceScript.jumpSqueezeMultiplier, 1).ToString();
                }

                if (juiceScript.landSqueezeMultiplier < 1) {
                    juiceSettingsReferences.landSquashText.text = "0";
                }
                else {
                    juiceSettingsReferences.landSquashText.text = System.Math.Round(juiceScript.landSqueezeMultiplier, 1).ToString();
                }

                juiceSettingsReferences.trailText.text = Mathf.Round(characterTrail.time).ToString();
                juiceSettingsReferences.tiltAngleText.text = Mathf.Round(juiceScript.maxTilt).ToString();
                juiceSettingsReferences.tiltSpeedText.text = Mathf.Round(juiceScript.tiltSpeed).ToString();
            }
        }

        private void OnEnable() {
            if (juiceSettingsReferences) {
                juiceSettingsReferences.runParticlesSlider.value = settingsHome.runParticles;
                juiceSettingsReferences.jumpParticlesSlider.value = settingsHome.jumpParticles;
                juiceSettingsReferences.landParticlesSlider.value = settingsHome.landParticles;

                juiceSettingsReferences.jumpSquashSlider.value = juiceScript.jumpSqueezeMultiplier;
                juiceSettingsReferences.landSquashSlider.value = juiceScript.landSqueezeMultiplier;

                juiceSettingsReferences.trailSlider.value = characterTrail.time;

                juiceSettingsReferences.tiltAngleSlider.value = juiceScript.maxTilt;
                juiceSettingsReferences.tiltSpeedSlider.value = juiceScript.tiltSpeed;

                juiceSettingsReferences.jumpSFXtoggle.isOn = settingsHome.jumpSFX.enabled;
                juiceSettingsReferences.landSFXtoggle.isOn = settingsHome.landSFX.enabled;
            }
            else {
                changeRunParticles(runParticlesValue);
                changeJumpParticles(jumpParticlesValue);
                changeLandParticles(landParticlesValue);
                changeJumpSquash(jumpSquashValue);
                changeLandSquash(landSquashValue);
                changeTrail(trailValue);
                changeTiltAmount(tiltAngleValue);
                changeTiltSpeed(tiltSpeedValue);
                settingsHome.jumpSFX.enabled = jumpSFXToggleValue;
                settingsHome.landSFX.enabled = landSFXToggleValue;
            }
        }


        public void changeRunParticles(float amount) {
            settingsHome.runParticles = amount;

            var em = runParticles.emission;
            em.rateOverDistance = settingsHome.runParticles;
        }



        public void changeJumpParticles(float amount) {
            settingsHome.jumpParticles = amount;

            var em = jumpParticles.emission;
            ParticleSystem.Burst newBurst = new ParticleSystem.Burst(0, settingsHome.jumpParticles);
            em.SetBurst(0, newBurst);
        }

        public void changeLandParticles(float amount) {
            settingsHome.landParticles = amount;

            var em = landParticles.emission;
            ParticleSystem.Burst newBurst = new ParticleSystem.Burst(0, settingsHome.landParticles);
            em.SetBurst(0, newBurst);
        }

        public void changeJumpSquash(float amount) {
            juiceScript.jumpSqueezeMultiplier = amount;
        }

        public void changeLandSquash(float amount) {
            juiceScript.landSqueezeMultiplier = amount;
        }

        public void changeTrail(float amount) {
            characterTrail.time = amount;
        }

        public void changeTiltAmount(float amount) {
            juiceScript.maxTilt = amount;
        }

        public void changeTiltSpeed(float amount) {
            juiceScript.tiltSpeed = amount;
        }
    }
}