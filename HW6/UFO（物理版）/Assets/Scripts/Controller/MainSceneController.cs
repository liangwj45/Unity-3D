using System.Collections.Generic;
using UnityEngine;

public class MainSceneController :MonoBehaviour, ISceneController {
    private List<GameObject> UFOs = new List<GameObject>();
    private GameModel model = new GameModel();
    private GameGUI gameGUI;
    private Ruler ruler;

    void Awake() {
        ruler = new Ruler();
        ruler.actionManager = gameObject.AddComponent<ActionManager>();
        gameGUI = gameObject.AddComponent<GameGUI>();
        gameGUI.onPressRestartButton += delegate {
            DestroyAll();
            model.Restart();
        };
        gameGUI.onPressNextRoundButton += delegate {
            if (model.gameState == GameState.Running) {
                model.NextRound();
            }
        };
        gameGUI.onPressNextTrialButton += delegate {
            if (model.gameState == GameState.Running) {
                model.NextTrial();
            }
        };
        gameGUI.onPressActionTypeButton += delegate {
            model.ChangeActionType();
        };
        model.onRefresh += delegate {
            gameGUI.state = model.gameState;
            gameGUI.round = model.currentRound;
            gameGUI.trial = model.currentTrial;
            gameGUI.score = model.score;
            gameGUI.type = model.type == ActionType.Physics ? "Physics" : "Kinematics";
            if (model.gameState != GameState.Running) {
                DestroyAll();
            }
        };
        Director.GetInstance().OnSceneWake(this);
    }

    void Update() {
        if (model.gameState == GameState.Running) {
            if (model.sceneState == SceneState.Shooting) {
                if (Input.GetButtonDown("Fire1")) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.tag == "UFO") {
                        OnHitUFO(hit.collider.gameObject);
                    }
                }

                for (int i = 0; i < UFOs.Count; i++) {
                    if (!UFOs[i].GetComponent<UFO>().visible) {
                        OnMissUFO(UFOs[i].gameObject);
                    }
                }

                if (UFOs.Count == 0 && model.gameState == GameState.Running) {
                    model.sceneState = SceneState.Waiting;
                    model.NextTrial();
                }
            } else {
                if (Input.GetKeyDown("space")) {
                    UFOs = ruler.GetUFOs(model.currentRound, model.type);
                    model.sceneState = SceneState.Shooting;
                }
            }
        }
    }

    public void LoadResources() { }

    private void OnHitUFO(GameObject ufo) {
        UFOs.Remove(ufo);
        UFOFactory.GetInstance().Recycle(ufo);
        model.AddScore(ufo.GetComponent<UFO>().score);
    }

    private void OnMissUFO(GameObject ufo) {
        UFOs.Remove(ufo);
        UFOFactory.GetInstance().Recycle(ufo);
        model.SubScore(ruler.GetSubScore(model.currentRound));
    }

    private void DestroyAll() {
        for (int i = 0; i < UFOs.Count; i++) {
            UFOFactory.GetInstance().Recycle(UFOs[i]);
        }
        UFOs.Clear();
    }
}
