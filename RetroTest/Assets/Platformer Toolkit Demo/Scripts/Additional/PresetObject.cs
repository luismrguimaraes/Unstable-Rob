using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GMTK.PlatformerToolkit {
    [CreateAssetMenu(menuName = "GMTK/Character Movement Preset", fileName = " - Character Movement Preset")]
    public class PresetObject : ScriptableObject {
        [SerializeField] string _presetName;
        [SerializeField] float _accel;
        [SerializeField] float _topSpeed;
        [SerializeField] float _decel;
        [SerializeField] float _turnSpeed;
        [SerializeField] float _jumpHeight;
        [SerializeField] float _timeToApex;
        [SerializeField] float _DMM;
        [SerializeField] float _airControl;
        [SerializeField] float _airControlActual;
        [SerializeField] float _airBrake;
        [SerializeField] bool _variableJH;
        [SerializeField] float _jumpCutoff;
        [SerializeField] int _doubleJump;



        public string PresetName => _presetName;
        public float Acceleration => _accel;
        public float TopSpeed => _topSpeed;
        public float Deceleration => _decel;
        public float TurnSpeed => _turnSpeed;
        public float JumpHeight => _jumpHeight;
        public float TimeToApex => _timeToApex;
        public float DownwardMovementMultiplier => _DMM;
        public float AirControl => _airControl;
        public float AirControlActual => _airControlActual;
        public float AirBrake => _airBrake;
        public bool VariableJumpHeight => _variableJH;
        public float JumpCutoff => _jumpCutoff;
        public int DoubleJump => _doubleJump;

        internal void Initialise(
            string presetName, float accel, float topSpeed, float decel, float turnSpeed,
            float jumpHeight, float timeToApex, float dMM, float airControl, float airControlActual,
            float airBrake, bool variableJH, float jumpCutoff, int doubleJump) {

            _presetName = presetName;
            _accel = accel;
            _topSpeed = topSpeed;
            _decel = decel;
            _turnSpeed = turnSpeed;
            _jumpHeight = jumpHeight;
            _timeToApex = timeToApex;
            _DMM = dMM;
            _airControl = airControl;
            _airControlActual = airControlActual;
            _airBrake = airBrake;
            _variableJH = variableJH;
            _jumpCutoff = jumpCutoff;
            _doubleJump = doubleJump;

        }
    }
}