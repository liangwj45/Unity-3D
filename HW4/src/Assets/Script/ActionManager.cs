using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager :SSActionManager, ISSActionCallback {
    public SSActionEventType complete = SSActionEventType.Completed;

    public void BoatMove(Boat boat) {
        complete = SSActionEventType.Started;
        Vector3 destination = boat.gameObject.transform.position;
        destination.x = -destination.x;
        CCMoveToAction action = CCMoveToAction.GetAction(destination, boat.speed);
        AddAction(boat.gameObject, action, this);
        boat.state = boat.state == BoatState.Left ? BoatState.Right : BoatState.Left;
    }

    public void UpDownBoat(Character character, Vector3 destination) {
        complete = SSActionEventType.Started;
        Vector3 position = character.gameObject.transform.position;
        Vector3 middle = position;
        if (destination.y > position.y) {
            middle.y = destination.y;
        } else {
            middle.x = destination.x;
        }
        SSAction action1 = CCMoveToAction.GetAction(middle, character.speed);
        SSAction action2 = CCMoveToAction.GetAction(destination, character.speed);
        SSAction sequence = CCSequenceAction.GetAction(1, 0, new List<SSAction> { action1, action2 });
        this.AddAction(character.gameObject, sequence, this);
    }

    public void ISSActionCallback(SSAction source) {
        complete = SSActionEventType.Completed;
    }
}