using UnityEngine;

public enum ActionType { Physics, Kinematics };  // 物理方法、运动学方法

public class Action :ScriptableObject {
    public GameObject gameObject;
    public Vector3 direction;
    public float speed = 0;
    public bool destroy = false;
    public bool enable = true;

    public virtual void Start() {
        throw new System.NotImplementedException();
    }

    public virtual void Update() {
        throw new System.NotImplementedException();
    }
}
