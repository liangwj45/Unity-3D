using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PreistDevil {
    public class GameGUI :MonoBehaviour {
        public IUserActionController userActionController;
        public int gameState;

        public void Start() {
            gameState = 0;
            userActionController = SSDirector.getInstance().currentScenceController as IUserActionController;
        }

        public void Restart() {
            gameState = 0;
        }

        public void OnGUI() {
            if (gameState == 1) {
                GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 30, 100, 50), "Gameover!");
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart")) {
                    gameState = 0;
                    userActionController.Restart();
                }
            } else if (gameState == 2) {
                GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 30, 100, 50), "Win!");
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart")) {
                    userActionController.Restart();
                }
            }
        }
    }
}