using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PreistDevil {
    public class Boat : IObject{
        public GameObject boat;
        public Vector3 location;
        public Vector3[] positions;
        public bool[] occupied;
        public int state;
        public Move moveComponent;

        public Boat(GameObject obj, ObjectType type) {
            boat = obj;
            location = new Vector3(2.1f, 0.55f, 0);
            SetPosition(location);
            SetScale(new Vector3(1.4f, 0.3f, 1));
            positions = new Vector3[2];
            occupied = new bool[2];
            state = 1;
            Init();
            moveComponent = boat.AddComponent(typeof(Move)) as Move;
        }

        public void Init() {
            for (int i = 0; i < 2; ++i) {
                positions[i] = new Vector3(location.x -0.3f * state +0.6f*i *state,  0.9f, 0); 
                occupied[i] = false;
            }
            SetPosition(location);
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
            Vector3 destination = location;
            destination.x = location.x * state;
            moveComponent.move = 1;
            moveComponent.SetDestination(destination);
        }

        public void MovePassager() {

        }

        public bool HasPassager() {
            return occupied[0] || occupied[1];
        }

        public void SetPosition(Vector3 position) {
            boat.transform.position = position;
        }

        public void SetScale(Vector3 scale) {
            boat.transform.localScale = scale;
        }
        public ObjectType GetObjectType() {
            return ObjectType.BOAT;
        }
    }
}