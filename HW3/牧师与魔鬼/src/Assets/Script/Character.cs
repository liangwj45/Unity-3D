using UnityEngine;

namespace PreistDevil {
    public class Character :IObject {
        public GameObject gameObject;
        public Vector3 position;
        public Character(GameObject obj, string name) {
            gameObject = obj;
            gameObject.name = name;
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
            SetPosition(position);
        }
    }
}