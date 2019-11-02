using System;
using UnityEngine;

public class GameGUI :MonoBehaviour {
    public bool lose = false;
    public int score = 0;

    public EventHandler onPressDirectionKey;
    public EventHandler onPressRestartButton;

    private void OnGUI() {
        GUI.Label(new Rect(10, Screen.height / 2 - 190, 200, 100), "Score: " + score, new GUIStyle() { fontSize = 20 });

        if (lose) {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 50), "You Lose!", new GUIStyle() { fontSize = 40, alignment = TextAnchor.MiddleCenter });
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", new GUIStyle("button") { fontSize = 30 })) {
                onPressRestartButton.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
