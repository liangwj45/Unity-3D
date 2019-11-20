using System.Collections.Generic;
using UnityEngine;

public class GameActionManager :ActionManager, IActionCallback {
    public GameObject player;
    public Dictionary<int, GameObject> props;
    public Dictionary<int, Action> tracert = new Dictionary<int, Action>();
    public Dictionary<int, Action> moveAround = new Dictionary<int, Action>();
    public float speed = 2f;
    public int room;
    public static System.Random rand = new System.Random();

    public void AddPropMoves(int sign) {
        room = sign;
        for (int i = 1; i <= 9; i++) {
            if (i == sign) {
                tracert.Add(i, Tracert(props[i]));
            } else {
                moveAround.Add(i, MoveAround(props[i], i));
            }
        }
    }

    public void SetPlayerArea(int sign) {
        tracert[room].destroy = true;
        tracert.Remove(room);
        moveAround[room] = MoveAround(props[room], room);
        room = sign;
        moveAround[room].destroy = true;
        moveAround.Remove(room);
        tracert[room] = Tracert(props[room]);
    }

    public Action MoveAround(GameObject prop, int sign) {
        Vector3 position = new Vector3((sign - 1) / 3 * 10 - 10, 0, (sign - 1) % 3 * 10 - 10);
        Action action1 = MoveToAction.GetAction(new Vector3(position.x - 2f, 0, position.z - 2f), speed);
        Action action2 = MoveToAction.GetAction(new Vector3(position.x - 2f, 0, position.z + 2f), speed);
        Action action3 = MoveToAction.GetAction(new Vector3(position.x + 2f, 0, position.z + 2f), speed);
        Action action4 = MoveToAction.GetAction(new Vector3(position.x + 2f, 0, position.z - 2f), speed);
        Action sequence = SequenceAction.GetAction(-1, 0, speed, new List<Action> { action1, action2, action3, action4 });
        AddAction(prop, sequence, this);
        return sequence;
    }

    public Action Tracert(GameObject prop) {
        Action action = TracertAction.GetAction(player, speed);
        AddAction(prop, action, this);
        return action;
    }

    public void IActionCallback(Action action) {

    }
}
