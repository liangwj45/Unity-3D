using UnityEngine;

public class Action :ScriptableObject {
    public GameObject gameObject;
    public bool destroy = false;
    public bool enable = true;
    public IActionCallback callback;

    public virtual void Start() {
        throw new System.NotImplementedException();
    }

    public virtual void Update() {
        throw new System.NotImplementedException();
    }
}
