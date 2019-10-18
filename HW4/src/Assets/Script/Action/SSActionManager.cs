using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager :MonoBehaviour {
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> add_list = new List<SSAction>();
    private List<int> delete_list = new List<int>();

    protected void Update() {
        foreach (SSAction ac in add_list) {
            actions[ac.GetInstanceID()] = ac;
        }
        add_list.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions) {
            SSAction ac = kv.Value;
            if (ac.destroy) {
                delete_list.Add(ac.GetInstanceID());
            } else if (ac.enable) {
                ac.Update();
            }
        }

        foreach (int key in delete_list) {
            SSAction ac = actions[key];
            actions.Remove(key);
            // DestroyObject(ac);
        }
        delete_list.Clear();
    }

    public void AddAction(GameObject gameObject, SSAction action, ISSActionCallback ICallBack) {
        action.gameObject = gameObject;
        action.transform = gameObject.transform;
        action.callback = ICallBack;
        add_list.Add(action);
        action.Start();
    }
}