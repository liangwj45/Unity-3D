using UnityEngine;


public enum BoatState {
    Left = -1, Right = 1
}

public class Boat :IObject {
    public GameObject gameObject;
    public Vector3 position;
    public bool[] occupied;
    public BoatState state;
    // public Move move;
    public float speed = 20f;

    public Boat(GameObject obj, string name) {
        gameObject = obj;
        gameObject.name = name;
        occupied = new bool[2];
    }

    public void Init() {
        occupied[0] = occupied[1] = false;
        SetPosition(position);
        state = BoatState.Right;
    }

    public Vector3 GetEmptyPosition() {
        for (int i = 0; i < 2; ++i) {
            if (!occupied[i]) {
                occupied[i] = true;
                return new Vector3(gameObject.transform.position.x - 0.3f + 0.6f * i, 0.9f, 0);
            }
        }
        // TODO: throw error
        return new Vector3(0, 0, 0);
    }

    public void ReleasePosition(Vector3 position) {
        if (position.x < gameObject.transform.position.x) {
            occupied[0] = false;
        } else {
            occupied[1] = false;
        }
    }

    public bool Full() {
        return occupied[0] && occupied[1];
    }

    public bool HasPassager() {
        return occupied[0] || occupied[1];
    }

    public void SetPosition(Vector3 position) {
        gameObject.transform.position = position;
        if (this.position == new Vector3(0, 0, 0)) {
            this.position = position;
        }
    }

    public void SetScale(Vector3 scale) {
        gameObject.transform.localScale = scale;
    }
}
