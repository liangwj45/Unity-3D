using System.Collections.Generic;
using UnityEngine;

public class SequenceAction :Action, IActionCallback {
    public List<Action> sequence;
    public int repeat = 1; // 1->only do it for once, -1->repeat forever
    public int start = 0;
    public float speed;

    private SequenceAction() { }

    public static SequenceAction GetAction(int repeat, int start, float speed, List<Action> sequence) {
        SequenceAction action = ScriptableObject.CreateInstance<SequenceAction>();
        action.sequence = sequence;
        action.repeat = repeat;
        action.start = start;
        action.speed = speed;
        return action;
    }

    public override void Update() {
        if (sequence.Count == 0) return;
        if (start < sequence.Count) {
            sequence[start].Update();
        }
    }

    public void IActionCallback(Action action) {
        action.destroy = false;
        start++;
        if (start >= sequence.Count) {
            start = 0;
            if (repeat > 0) repeat--;
            if (repeat == 0) {
                destroy = true;
                callback.IActionCallback(this);
            }
        }
    }

    public override void Start() {
        foreach (Action action in sequence) {
            action.gameObject = gameObject;
            action.callback = this;
            action.Start();
        }
    }
}
