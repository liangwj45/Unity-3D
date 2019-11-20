using UnityEngine;

public class TracertAction :Action {
    public GameObject target;
    public float speed;

    private TracertAction() { }

    public static TracertAction GetAction(GameObject target, float speed) {
        TracertAction action = ScriptableObject.CreateInstance<TracertAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }

    public override void Update() {
        Quaternion rotation = Quaternion.LookRotation(target.transform.position - gameObject.transform.position, Vector3.up);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, speed * Time.deltaTime);
        gameObject.transform.rotation = rotation;
        if (gameObject.transform.position == target.transform.position) {
            destroy = true;
        }
    }

    public override void Start() { }
}
