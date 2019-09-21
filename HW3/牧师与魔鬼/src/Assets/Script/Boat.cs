using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PreistDevil {
    public class Boat :IObject {
        public GameObject gameObject;
        public Vector3 position;
        public Vector3[] positions;
        public bool[] occupied;
        public int state;
        public Move moveComponent;

        public Boat(GameObject obj, string name) {
            gameObject = obj;
            gameObject.name = name;
            positions = new Vector3[2];
            occupied = new bool[2];
            state = 1;
            Init();
            moveComponent = gameObject.AddComponent(typeof(Move)) as Move;
            gameObject.AddComponent(typeof(ClickGUI));
        }

        public void Init() {
            for (int i = 0; i < 2; ++i) {
                positions[i] = new Vector3(position.x - 0.3f * state + 0.6f * i * state, 0.9f, 0);
                occupied[i] = false;
            }
            SetPosition(position);
            state = 1;
        }

        public Vector3 GetEmptyPosition() {
            for (int i = 0; i < 2; ++i) {
                if (!occupied[i]) {
                    occupied[i] = true;
                    return positions[i];
                }
            }
            // TODO: throw error
            return new Vector3(0, 0, 0);
        }

        public bool ReleasePosition(Vector3 position) {
            for (int i = 0; i < 2; ++i) {
                if (positions[i] == position && occupied[i]) {
                    occupied[i] = false;
                    return true;
                }
            }
            return false;
        }

        public void MoveBoat() {
            state = -state;
            Vector3 destination = position;
            destination.x = position.x * state;
            moveComponent.move = 1;
            moveComponent.SetDestination(destination);
        }

        public void MovePassager() {

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
}