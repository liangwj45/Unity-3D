using UnityEngine;

namespace PreistDevil {
    public enum CharacterState : uint {
        OnBoat, OnCoastL, OnCoastR
    }

    public class Character :IObject {
        public GameObject gameObject;
        public Vector3 position;
        public Move move;
        public ClickGUI clickGUI;
        public CharacterState state;

        public Character(GameObject obj, string name) {
            gameObject = obj;
            gameObject.name = name;
            state = CharacterState.OnCoastR;
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

        public void Init() {
            gameObject.transform.parent = null;
            state = CharacterState.OnCoastR;
            SetPosition(position);
        }

        public void MoveToPosition(Vector3 destination) {
            move.SetDestination(destination);
        }
    }
}