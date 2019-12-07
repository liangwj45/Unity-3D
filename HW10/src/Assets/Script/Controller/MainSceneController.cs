﻿using System;
using UnityEngine;


public class MainSceneController : MonoBehaviour, ISceneController, IUserActionController {
    public Water water;
    public Coast coastL;
    public Coast coastR;
    public Character[] preists;
    public Character[] devils;
    public Boat boat;
    public GameGUI gameGUI;
    public ActionManager actionManager;
    public Judger judger;
    public AISystem ai;

    void Awake() {
        gameGUI = gameObject.AddComponent<GameGUI>() as GameGUI;
        actionManager = gameObject.AddComponent<ActionManager>() as ActionManager;
        preists = new Character[3];
        devils = new Character[3];
        SSDirector.getInstance().currentScenceController = this;
        LoadResources();

        judger = gameObject.AddComponent<Judger>() as Judger;
        judger.preists = preists;
        judger.devils = devils;
        judger.boat = boat;
        judger.sceneController = this;

        ai = new AISystem(3);
        gameGUI.onPressTipButton += delegate {
            int cnt1 = 0, cnt2 = 0;
            for (int i = 0; i < preists.Length; i++) {
                if (!preists[i].onLeft) {
                    cnt1++;
                }
            }
            for (int i = 0; i < devils.Length; i++) {
                if (!devils[i].onLeft) {
                    cnt2++;
                }
            }
            Tuple<int, int> tip = ai.GetNextStep(cnt1, cnt2, boat.onLeft);
            gameGUI.boatOnLeft = boat.onLeft;
            gameGUI.tip = tip;
        };
    }

    public void LoadResources() {
        coastL = new Coast(Instantiate(Resources.Load("Prefabs/Coast")) as GameObject, "left");
        coastL.SetPosition(new Vector3(-4.7f, 0.5f, 0));
        coastL.SetScale(new Vector3(3.8f, 1, 1));
        coastL.Init(false);

        coastR = new Coast(Instantiate(Resources.Load("Prefabs/Coast")) as GameObject, "right");
        coastR.SetPosition(new Vector3(4.7f, 0.5f, 0));
        coastR.SetScale(new Vector3(3.8f, 1, 1));
        coastR.Init(false);

        boat = new Boat(Instantiate(Resources.Load("Prefabs/Boat")) as GameObject, "boat");
        boat.gameObject.AddComponent(typeof(ClickGUI));
        boat.SetPosition(new Vector3(2.1f, 0.55f, 0));
        boat.SetScale(new Vector3(1.4f, 0.3f, 1));
        boat.Init();

        for (int i = 0; i < 3; ++i) {
            preists[i] = new Character(Instantiate(Resources.Load("Prefabs/Preist")) as GameObject, "preist");
            ClickGUI clickGUI = preists[i].gameObject.AddComponent(typeof(ClickGUI)) as ClickGUI;
            clickGUI.character = preists[i];
            preists[i].clickGUI = clickGUI;
            preists[i].clickGUI.character = preists[i];
            preists[i].SetPosition(coastR.GetEmptyPosition());
            preists[i].SetScale(new Vector3(0.4f, 0.4f, 0.4f));
            preists[i].onLeft = false;
        }

        for (int i = 0; i < 3; ++i) {
            devils[i] = new Character(Instantiate(Resources.Load("Prefabs/Devil")) as GameObject, "devil");
            ClickGUI clickGUI = devils[i].gameObject.AddComponent(typeof(ClickGUI)) as ClickGUI;
            clickGUI.character = preists[i];
            devils[i].clickGUI = clickGUI;
            devils[i].clickGUI.character = devils[i];
            devils[i].SetPosition(coastR.GetEmptyPosition());
            devils[i].SetScale(new Vector3(0.4f, 0.4f, 0.4f));
            devils[i].onLeft = false;
        }
    }

    public void UpDownBoat(Character character) {
        Vector3 position = new Vector3(0, 0, 0);
        if (character.state == CharacterState.OnBoat) {
            boat.ReleasePosition(character.gameObject.transform.position);
            character.gameObject.transform.parent = null;
            if (!boat.onLeft) {
                position = coastR.GetEmptyPosition();
                character.state = CharacterState.OnCoastR;
            } else {
                position = coastL.GetEmptyPosition();
                character.state = CharacterState.OnCoastL;
            }
            actionManager.UpDownBoat(character, position);
        } else {
            if (boat.Full()) return;
            if ((character.state == CharacterState.OnCoastL && boat.onLeft) || (character.state == CharacterState.OnCoastR && !boat.onLeft)) {
                position = boat.GetEmptyPosition();
                actionManager.UpDownBoat(character, position);
                character.gameObject.transform.parent = boat.gameObject.transform;
                character.state = CharacterState.OnBoat;
                if (boat.onLeft) coastL.ReleasePosition(character.gameObject.transform.position);
                else coastR.ReleasePosition(character.gameObject.transform.position);
            }
        }
    }

    public void MoveBoat() {
        if (boat.HasPassager()) {
            Vector3 destination = boat.gameObject.transform.position;
            destination.x = -destination.x;
            actionManager.BoatMove(boat);
            for (int i = 0; i < preists.Length; i++) {
                if (preists[i].state == CharacterState.OnBoat) {
                    preists[i].onLeft = boat.onLeft;
                }
            }
            for (int i = 0; i < devils.Length; i++) {
                if (devils[i].state == CharacterState.OnBoat) {
                    devils[i].onLeft = boat.onLeft;
                }
            }
        }
    }

    public void Restart() {
        gameGUI.Restart();
        coastL.Init(false);
        coastR.Init(true);
        for (int i = 0; i < 3; ++i) {
            preists[i].Init();
            devils[i].Init();
        }
        boat.Init();
    }

    public void UpdateGameState(GameState gameState) {
        gameGUI.gameState = gameState;
    }
}