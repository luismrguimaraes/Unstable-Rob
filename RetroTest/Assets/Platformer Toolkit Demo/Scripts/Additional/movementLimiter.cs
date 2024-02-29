using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GMTK.PlatformerToolkit {
    public class movementLimiter : MonoBehaviour {
        public static movementLimiter instance;

        [SerializeField] bool _initialCharacterCanMove = true;
        public bool CharacterCanMove;

        private void OnEnable() {
            instance = this;
        }

        private void Start() {
            CharacterCanMove = _initialCharacterCanMove;
        }
    }
}