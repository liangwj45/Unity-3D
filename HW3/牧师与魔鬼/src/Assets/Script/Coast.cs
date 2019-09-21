using UnityEngine;

namespace PreistDevil {
    public class Coast :IObject {
        public GameObject coast;
        public Vector3[] positions;
        public bool[] occupied;
        public int state;

        public Coast(GameObject obj, string side) {
            state = side == "left" ? -1 : 1;
            coast = obj;
            SetPosition(new Vector3(4.7f * state, 0.5f, 0));
            SetScale(new Vector3(3.8f, 1, 1));
            positions = new Vector3[6];
            occupied = new bool[6];
            Init();
        }

        public void Init() {
            for (int i = 0; i < 6; ++i) {
                positions[i] = new Vector3(state * (3.4f + 0.6f * i), 1.2f, 0);
                occupied[i] = false;
            }
        }

        public Vector3 GetEmptyPosition() {
            for (int i = 0; i < 6; ++i) {
                if (!occupied[i]) {
                    occupied[i] = true;
                    return positions[i];
                }
            }
            // TODO: throw error
            return new Vector3(0, 0, 0);
        }

        public bool ReleasePosition(Vector3 position) {
            for (int i = 0; i < 6; ++i) {
                if (positions[i] == position && occupied[i]) {
                    occupied[i] = false;
                    return true;
                }
            }
            return false;
        }

        public void SetPosition(Vector3 position) {
            coast.transform.position = position;
        }

        public void SetScale(Vector3 scale) {
            coast.transform.localScale = scale;
        }
        public ObjectType GetObjectType() {
            return ObjectType.COAST;
        }
    }
}