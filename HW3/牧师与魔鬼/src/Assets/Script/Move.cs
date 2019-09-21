using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PreistDevil {
    public class Move :MonoBehaviour {
        public float speed = 20f;
        public Vector3 target;
        public int move = 0;

        void Update() {
            if (move == 0) return;
            if (transform.position != target) {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }

        public void SetDestination(Vector3 destination) {
            target = destination;
        }
    }
}