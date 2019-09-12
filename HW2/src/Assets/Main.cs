using UnityEngine;

public class Main :MonoBehaviour {
    private int[,] check = new int[3, 3]; // 记录胜负情况
    private int[,] map = new int[3, 3]; // 记录棋子信息
    public int turn = 1, count = 0, win = 0;

    void Start() {
        turn = 1; count = 0; win = 0;
        for (int i = 0; i < 3; ++i)
            for (int j = 0; j < 3; ++j) {
                check[i, j] = 0; map[i, j] = 0;
            }
    }

    private void OnGUI() {
        if (GUI.Button(new Rect(900, 650, 100, 60), "Reset")) Start();

        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.alignment = TextAnchor.MiddleCenter;
        style.fontStyle = FontStyle.BoldAndItalic;
        style.normal.textColor = Color.red;

        if (win == 1) GUI.Label(new Rect(900, 180, 100, 100), "Player1 WIN", style);
        else if (win == 2) GUI.Label(new Rect(900, 180, 100, 100), "Player2 WIN", style);
        else if (win == 3) GUI.Label(new Rect(900, 180, 100, 100), "TIE", style);

        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                if (map[i, j] == 1) GUI.Button(new Rect(i * 100 + 0, j * 100 + 300, 100, 100), "O", style: style);
                else if (map[i, j] == -1) GUI.Button(new Rect(i * 100 + 0, j * 100 + 300, 100, 100), "X", style: style);
                if (GUI.Button(new Rect(i * 100 + 0, j * 100 + 300, 100, 100), "")) {
                    if (win > 0) return;
                    if (System.Math.Abs((check[0, i] += turn)) == 3) win = turn > 0 ? 1 : 2;
                    if (System.Math.Abs((check[1, j] += turn)) == 3) win = turn > 0 ? 1 : 2;
                    if (i == j && System.Math.Abs((check[2, 0] += turn)) == 3) win = turn > 0 ? 1 : 2;
                    if (i + j == 2 && System.Math.Abs((check[2, 1] += turn)) == 3) win = turn > 0 ? 1 : 2;
                    if (win == 0 && ++count == 9) win = 3;
                    map[i, j] = turn; turn = -turn;
                }
            }
        }
    }
}
