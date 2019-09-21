using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PreistDevil {
    public interface IUserActionController {
        void MoveBoat();
        void UpDownBoat(Character character);
        void Restart();
    }
}
