using System;
using UnityEngine;

public class GameModel {
    static GameModel instance = new GameModel();
    public EventHandler onReFresh;
    public EventHandler onGameover;
    public GameObject player;
    public bool lose = false;
    public int score = 0;

    private GameModel() { }

    public static GameModel GetInstance() {
        return instance ?? new GameModel();
    }

    public void Restart() {
        lose = false;
        score = 0;
        onReFresh.Invoke(this, EventArgs.Empty);
    }

    public void PlayerEscape() {
        score++;
        onReFresh.Invoke(this, EventArgs.Empty);
    }

    public void GameOver() {
        lose = true;
        onGameover.Invoke(this, EventArgs.Empty);
        onReFresh.Invoke(this, EventArgs.Empty);
    }
}
