using System;
using UnityEngine;

public enum GameState : uint {
    Continue, Win, Gameover
}
public class GameGUI : MonoBehaviour {
    public IUserActionController userActionController;
    public GameState gameState;
    public bool boatOnLeft;
    public EventHandler onPressTipButton;
    public string tipMsg = "";
    public Tuple<int, int> tip = Tuple.Create(0, 0);

    public void Start() {
        gameState = GameState.Continue;
        userActionController = SSDirector.getInstance().currentScenceController as IUserActionController;
    }

    public void Restart() {
        gameState = GameState.Continue;
    }

    public void OnGUI() {
        if (gameState == GameState.Continue) {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 5, 300, 50), tipMsg);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 10, 140, 70), "Tip")) {
                onPressTipButton.Invoke(this, EventArgs.Empty);
                if (tip.Item1 != 0 || tip.Item2 != 0) {
                    tipMsg = string.Format("Move {0} Preists and {1} Devils to the {2} Coast", tip.Item1, tip.Item2, boatOnLeft ? "Rigth" : "Left");
                }
            }
        } else {
            string msg = gameState == GameState.Gameover ? "Gameover!" : "Win!";
            GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 + 15, 100, 50), msg);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 60, 140, 70), "Restart")) {
                userActionController.Restart();
            }
        }
    }
}
