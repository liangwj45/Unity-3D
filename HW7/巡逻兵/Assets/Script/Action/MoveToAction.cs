                                using UnityEngine;

public class MoveToAction :Action {
    public Vector3 destination;
    public float speed;

    private MoveToAction() { }

    public static MoveToAction GetAction(Vector3 destination, float speed) {
        MoveToAction action = ScriptableObject.CreateInstance<MoveToAction>();
        action.destination = destination;
        action.speed = speed;
        return action;
    }

    public override void Update() {
        Quaternion rotation = Quaternion.LookRotation(destination - gameObject.transform.position, Vector3.up);
        gameObject.transform.rotation = rotation;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, speed * Time.deltaTime);
        if (gameObject.transform.position == destination) {
            destroy = true;
            callback.IActionCallback(this);
        }
    }

    public override void Start() { }
}
