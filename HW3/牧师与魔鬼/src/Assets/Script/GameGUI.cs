using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PreistDevil {
    public class GameGUI :MonoBehaviour {
        public int state;
        void Start() {
            state = 0;
        }

        public void OnGUI() {
            if (state == 1) {
                GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 30, 100, 50), "Gameover!");
            } else if (state == 2) {
                GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 30, 100, 50), "Win!");
            }
        }
    }
}