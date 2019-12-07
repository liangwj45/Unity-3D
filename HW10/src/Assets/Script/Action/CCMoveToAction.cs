﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction :SSAction {
    public Vector3 target;
    public float speed;

    public static CCMoveToAction GetAction(Vector3 target, float speed) {
        CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }

    public override void Update() {
        this.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target) {
            destroy = true;
            callback.ISSActionCallback(this);
        }
    }

    public override void Start() {

    }
}
