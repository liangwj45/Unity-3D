﻿using UnityEngine;

public enum CharacterState : uint {
    OnBoat, OnCoastL, OnCoastR
}

public class Character : IObject {
    public GameObject gameObject;
    public Vector3 position;
    // public Move move;
    public ClickGUI clickGUI;
    public CharacterState state;
    public bool onLeft = false;
    public float speed = 20f;

    public Character(GameObject obj, string name) {
        gameObject = obj;
        gameObject.name = name;
        state = CharacterState.OnCoastR;
    }

    public void SetPosition(Vector3 position) {
        gameObject.transform.position = position;
        if (this.position == new Vector3(0, 0, 0)) {
            this.position = position;
        }
    }

    public void SetScale(Vector3 scale) {
        gameObject.transform.localScale = scale;
    }

    public void Init() {
        gameObject.transform.parent = null;
        state = CharacterState.OnCoastR;
        onLeft = false;
        SetPosition(position);
    }
}