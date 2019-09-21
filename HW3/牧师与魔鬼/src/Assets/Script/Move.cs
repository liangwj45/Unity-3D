using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PreistDevil {
    public enum MoveState :uint {
        Stoped, MovingToMiddle, MovingToDestination
    }
    public class Move :MonoBehaviour {
        public float speed = 20f;
        public Vector3 destination;
        public Vector3 middle;
        public MoveState state = MoveState.Stoped;

        void Update() {
            if (state == MoveState.Stoped) return;
            if (state == MoveState.MovingToMiddle) {
                if (transform.position == middle) {
                    state = MoveState.MovingToDestination;
                } else {
                    transform.position = Vector3.MoveTowards(transform.position, middle, speed * Time.deltaTime);
                }
            } else {
                if (transform.position == destination) {
                    state = MoveState.Stoped;
                } else {
                    transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                }
            }
        }

        public void SetDestination(Vector3 destination) {
            this.destination = middle = destination;
            if (transform.position.y < destination.y) {
                middle.x = transform.position.x;
                state = MoveState.MovingToMiddle;
            } else if (transform.position.y > destination.y) {
                middle.y = transform.position.y;
                state = MoveState.MovingToMiddle;
            } else {
                state = MoveState.MovingToDestination;
            }
        }
    }
}