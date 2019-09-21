using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PreistDevil {
    public interface IUserAction {
        void MoveBoat();
        void ObjectIsClicked(IObject obj);
        void Restart();
    }
}
