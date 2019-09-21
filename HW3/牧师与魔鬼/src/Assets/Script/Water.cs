using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PreistDevil {
    public class Water :IObject {
        public GameObject water;
        public Water(GameObject obj) {
            water = obj;
            SetPosition(new Vector3(0, 0.2f, 0));
            SetScale(new Vector3(5.6f, 0.4f, 1));
        }

        public void SetPosition(Vector3 position) {
            water.transform.position = position;
        }

        public void SetScale(Vector3 scale) {
            water.transform.localScale = scale;
        }

        public ObjectType GetObjectType() {
            return ObjectType.WATER;
        }
    }
}