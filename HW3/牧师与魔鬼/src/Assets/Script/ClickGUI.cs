using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PreistDevil {
    public class ClickGUI :MonoBehaviour {
        IUserActionController userActionController;

        void Start() {
            userActionController = SSDirector.getInstance().currentScenceController as IUserActionController;
        }

        public void OnMouseDown() {
            if (gameObject.name == "boat") {
                userActionController.MoveBoat();
            } else {
                userActionController.UpDownBoat(gameObject);
            }
        }
    }
}
