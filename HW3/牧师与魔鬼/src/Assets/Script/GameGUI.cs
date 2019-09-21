using UnityEngine;

namespace PreistDevil {
    public enum GameState :uint {
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
            if (gameState == GameState.Continue) return;
            string msg = gameState == GameState.Gameover ? "Gameover!" : "Win!";
            GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 + 15, 100, 50), msg);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 60, 140, 70), "Restart")) {
                userActionController.Restart();
            }
        }
    }
}