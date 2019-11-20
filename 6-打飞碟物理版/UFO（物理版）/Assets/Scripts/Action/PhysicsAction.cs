using UnityEngine;

public class PhysicsAction :Action {
    public static PhysicsAction GetAction(Vector3 direction, float speed) {
        PhysicsAction action = ScriptableObject.CreateInstance<PhysicsAction>();
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
            Destroy(gameObject.GetComponent<Rigidbody>());
            destroy = true;
            enable = false;
        }
    }

    public override void Start() { }
}
