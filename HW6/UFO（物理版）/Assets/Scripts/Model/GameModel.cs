using System;

public enum SceneState { Waiting, Shooting }
public enum GameState { Running, Lose, Win }

public class GameModel {
    public ActionType type = ActionType.Kinematics;
    public readonly int maxRound = 10;
    public readonly int maxTrial = 10;
    public EventHandler onRefresh;

    public GameState gameState = GameState.Running;
    public SceneState sceneState = SceneState.Waiting;
    public int currentRound = 1;
    public int currentTrial = 1;
    public int score = 0;

    public void Restart() {
        gameState = GameState.Running;
        sceneState = SceneState.Waiting;
        currentRound = 1;
        currentTrial = 1;
        score = 0;
        onRefresh.Invoke(this, EventArgs.Empty);
    }

    public void NextRound() {
        if (currentRound >= maxRound) {
            gameState = GameState.Win;
            onRefresh.Invoke(this, EventArgs.Empty);
        } else {
            currentRound++;
            onRefresh.Invoke(this, EventArgs.Empty);
        }
    }

    public void NextTrial() {
        if (currentTrial >= maxTrial) {
            currentTrial = 1;
            NextRound();
        } else {
            currentTrial++;
            onRefresh.Invoke(this, EventArgs.Empty);
        }
    }

    public void ChangeActionType() {
        type = type == ActionType.Physics ? ActionType.Kinematics : ActionType.Physics;
        onRefresh.Invoke(this, EventArgs.Empty);
    }

    public void AddScore(int score) {
        this.score += score;
        onRefresh.Invoke(this, EventArgs.Empty);
    }

    public void SubScore(int score) {
        this.score -= score;
        if (this.score < 0) {
            gameState = GameState.Lose;
        }
        onRefresh.Invoke(this, EventArgs.Empty);
    }
}
