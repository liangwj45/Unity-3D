using UnityEngine;

namespace PreistDevil {
    public class Coast :IObject {
        public GameObject gameObject;
        public Vector3[] positions;
        public bool[] occupied;
        public int state;

        public Coast(GameObject obj, string side) {
            gameObject = obj;
            state = side == "left" ? -1 : 1;
            positions = new Vector3[6];
            occupied = new bool[6];
            for (int i = 0; i < 6; ++i) {
                positions[i] = new Vector3(state * (3.4f + 0.6f * i), 1.2f, 0);
            }
        }

        public void Init(bool occupied) {
            for (int i = 0; i < 6; ++i) {
                this.occupied[i] = occupied;
            }
        }

        public Vector3 GetEmptyPosition() {
            for (int i = 0; i < 6; ++i) {
                if (!occupied[i]) {
                    occupied[i] = true;
                    return positions[i];
                }
            }
            return new Vector3(0, 0, 0);
        }

        public bool ReleasePosition(Vector3 position) {
            for (int i = 0; i < 6; ++i) {
                if (positions[i] == position) {
                    occupied[i] = false;
                    return true;
                }
            }
            return false;
        }

        public void SetPosition(Vector3 position) {
            gameObject.transform.position = position;
        }

        public void SetScale(Vector3 scale) {
            gameObject.transform.localScale = scale;
        }
    }
}