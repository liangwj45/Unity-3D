using UnityEngine;


namespace PreistDevil {
    public class Character :IObject {
        public GameObject character;
        public ObjectType type;
        public Character(GameObject obj, ObjectType type) {
            character = obj;
            this.type = type;
            SetScale(new Vector3(0.4f, 0.4f, 0.4f));
            character.AddComponent(typeof(Move));
        }

        public void SetPosition(Vector3 position) {
            character.transform.position = position;
        }

        public void SetScale(Vector3 scale) {
            character.transform.localScale = scale;
        }

        public ObjectType GetObjectType() {
            return type;
        }
    }
}