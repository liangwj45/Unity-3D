using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SceneState { Waiting, Shooting }
public enum GameState { Running, Lose, Win }

public class GameModel
{

  public GameState gameState = GameState.Running;
  public SceneState sceneState = SceneState.Waiting;
  public EventHandler onRefresh;
  public EventHandler onEnterNextTrial;
  public readonly int maxRound = 10;
  public readonly int maxTrial = 10;
  public int currentRound { get; private set; } = 1;
  public int currentTrial { get; private set; } = 1;
  public int score { get; private set; } = 0;

  public void Restart()
  {
    gameState = GameState.Running;
    sceneState = SceneState.Waiting;
    currentRound = 1;
    currentTrial = 1;
    score = 0;
    onRefresh.Invoke(this, EventArgs.Empty);
    onEnterNextTrial.Invoke(this, EventArgs.Empty);
  }

  public void NextRound()
  {
    if (currentRound == maxRound)
    {
      gameState = GameState.Win;
    }
    else
    {
      currentRound++;
      onRefresh.Invoke(this, EventArgs.Empty);
      onEnterNextTrial.Invoke(this, EventArgs.Empty);
    }
  }

  public void NextTrial()
  {
    if (currentTrial == maxTrial)
    {
      currentTrial = 1;
      NextRound();
    }
    else
    {
      currentTrial++;
      onRefresh.Invoke(this, EventArgs.Empty);
      onEnterNextTrial.Invoke(this, EventArgs.Empty);
    }
  }

  public void AddScore(int score)
  {
    this.score += score;
    onRefresh.Invoke(this, EventArgs.Empty);
  }

  public void SubScore(int score)
  {
    this.score -= score;
    if (score < 0)
    {
      gameState = GameState.Lose;
    }
    onRefresh.Invoke(this, EventArgs.Empty);
  }
}