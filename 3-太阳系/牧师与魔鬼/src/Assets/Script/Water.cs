using UnityEngine;

namespace PreistDevil {
    public class Water :IObject {
        public GameObject gameObject;
        public Water(GameObject obj) {
            gameObject = obj;
        }

        public void SetPosition(Vector3 position) {
            gameObject.transform.position = position;
        }

        public void SetScale(Vector3 scale) {
            gameObject.transform.localScale = scale;
        }

        public void Init() { }
    }
}