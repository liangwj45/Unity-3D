using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judger :MonoBehaviour {
    public Character[] preists;
    public Character[] devils;
    public Boat boat;
    public GameState gameState;
    public ISceneController sceneController;

    public void Start() {
        gameState = GameState.Continue;
    }

    public void Update() {
        GameState state = Check();

        // Debug.Log(gameState);
        if (gameState != state) {
            sceneController.UpdateGameState(state);
            gameState = state;
        }
    }

    public GameState Check() {
        int preist_left = 0, preist_right = 0, devil_left = 0, devil_right = 0, win = 0;
        for (int i = 0; i < 3; ++i) {
            if (preists[i].state == CharacterState.OnCoastL) {
                preist_left++; win++;
            } else if (preists[i].state == CharacterState.OnBoat && boat.state == BoatState.Left) {
                preist_left++;
            } else {
                preist_right++;
            }
            if (devils[i].state == CharacterState.OnCoastL) {
                devil_left++; win++;
            } else if (devils[i].state == CharacterState.OnBoat && boat.state == BoatState.Left) {
                devil_left++;
            } else {
                devil_right++;
            }
        }
        if (win == 6) return GameState.Win;
        if ((preist_left < devil_left && preist_left != 0) || (preist_right < devil_right && preist_right != 0)) return GameState.Gameover;
        return GameState.Continue;
    }
}
