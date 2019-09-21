using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PreistDevil {
    public enum GameState : uint {
        Continue, Win, Gameover
    }
    public class GameGUI :MonoBehaviour {
        public IUserActionController userActionController;
        public GameState gameState;

        public void Start() {
            gameState = GameState.Continue;
            userActionController = SSDirector.getInstance().currentScenceController as IUserActionController;
        }

        public void Restart() {
            gameState = GameState.Continue;
        }

        public void OnGUI() {
            if (gameState == GameState.Gameover) {
                GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 4, 100, 50), "Gameover!");
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 3, 140, 70), "Restart")) {
                    gameState = 0;
                    userActionController.Restart();
                }
            } else if (gameState == GameState.Win) {
                GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 4, 100, 50), "Win!");
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 3, 140, 70), "Restart")) {
                    userActionController.Restart();
                }
            }
        }
    }
}