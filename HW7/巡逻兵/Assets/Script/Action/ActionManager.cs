using System.Collections.Generic;
using UnityEngine;

public class ActionManager :MonoBehaviour {
    private List<Action> actions = new List<Action>();
    private List<Action> add_list = new List<Action>();
    private List<Action> delete_list = new List<Action>();

    protected void Update() {
        foreach (Action ac in add_list) {
            actions.Add(ac);
        }
        add_list.Clear();

        foreach (Action ac in actions) {
            if (ac.destroy) {
                delete_list.Add(ac);
            } else if (ac.enable) {
                ac.Update();
            }
        }

        foreach (Action ac in delete_list) {
            actions.Remove(ac);
        }

        delete_list.Clear();
    }

    public void AddAction(GameObject gameObject, Action action, IActionCallback callback) {
        action.gameObject = gameObject;
        action.callback = callback;
        add_list.Add(action);
        action.Start();
    }

    public void StopAll() {
        foreach (Action ac in actions) {
            ac.enable = false;
        }
    }

    public void StartAll() {
        foreach (Action ac in actions) {
            if (!ac.destroy) {
                ac.enable = true;
            }
        }
    }
}
