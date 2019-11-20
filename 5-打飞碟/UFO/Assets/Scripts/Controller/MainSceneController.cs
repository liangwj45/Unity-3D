using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour, ISceneController
{
  private GameModel model = new GameModel();
  private GameGUI gameGUI;
  private Ruler ruler;

  private List<GameObject> UFOs = new List<GameObject>();

  void Awake()
  {
    Director.GetInstance().OnSceneWake(this);
    gameGUI = gameObject.AddComponent<GameGUI>();
    gameGUI.onPressRestartButton += delegate
    {
      model.Restart();
    };
    gameGUI.onPressNextRoundButton += delegate
    {
      if (model.gameState == GameState.Running)
      {
        model.NextRound();
      }
    };
    gameGUI.onPressNextTrialButton += delegate
    {
      if (model.gameState == GameState.Running)
      {
        model.NextTrial();
      }
    };
    ruler = new Ruler(model.currentRound);
    model.onRefresh += delegate
    {
      gameGUI.state = model.gameState;
      gameGUI.round = model.currentRound;
      gameGUI.trial = model.currentTrial;
      gameGUI.score = model.score;
      if (model.gameState == GameState.Lose)
      {
        DestroyAll();
      }
    };
    model.onEnterNextTrial += delegate
    {
      ruler = new Ruler(model.currentRound);
    };
  }

  void Update()
  {
    for (int i = 0; i < UFOs.Count; i++)
    {
      if (UFOs[i].GetComponent<UFO>().speed == 0)
      {
        OnMissUFO(UFOs[i].gameObject);
      }
    }

    if (model.gameState == GameState.Running)
    {
      if (model.sceneState == SceneState.Shooting && Input.GetButtonDown("Fire1"))
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.tag == "UFO")
        {
          OnHitUFO(hit.collider.gameObject);
        }
      }

      if (model.sceneState == SceneState.Waiting && Input.GetKeyDown("space"))
      {
        model.sceneState = SceneState.Shooting;
        model.NextTrial();
        if (model.gameState == GameState.Win)
        {
          return;
        }
        UFOs.AddRange(ruler.GetUFOs());
      }

      if (UFOs.Count == 0)
      {
        model.sceneState = SceneState.Waiting;
      }
    }
  }

  public void LoadResources() { }

  private void OnHitUFO(GameObject ufo)
  {
    model.AddScore(ufo.GetComponent<UFO>().score);
    DestroyUFO(ufo);
  }

  private void OnMissUFO(GameObject ufo)
  {
    model.SubScore();
    DestroyUFO(ufo);
  }

  private void DestroyUFO(GameObject ufo)
  {
    UFOs.Remove(ufo);
    UFOFactory.GetInstance().Recycle(ufo);
  }

  private void DestroyAll()
  {
    for (int i = 0; i < UFOs.Count; i++)
    {
      UFOFactory.GetInstance().Recycle(UFOs[i]);
    }
    UFOs.Clear();
  }
}