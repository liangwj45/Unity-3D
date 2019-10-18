using UnityEngine;

public class KinematicsAction :Action {
    public static KinematicsAction GetAction(Vector3 direction, float speed) {
        KinematicsAction action = ScriptableObject.CreateInstance<KinematicsAction>();
        action.direction = direction;
        action.speed = speed;
        action.destroy = false;
        action.enable = true;
        return action;
    }

    public override void Update() {
        if (gameObject.GetComponent<UFO>().visible) {
            gameObject.transform.Translate(speed * direction * Time.deltaTime);
        } else {
            speed = 0;
            destroy = true;
            enable = false;
        }
    }

    public override void Start() { }
}
