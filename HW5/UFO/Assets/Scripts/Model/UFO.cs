using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UFOState { Stop, Moving };
public class UFO :MonoBehaviour {
    public int score;
    public float speed = 0;
    public Vector3 direction { get; set; }
    public UFOState state;

    public void Update() {
        if (state == UFOState.Moving) {
            gameObject.transform.Translate(speed * direction * Time.deltaTime);
        } else {
            speed = 0;
        }
    }

    public void SetPosition(Vector3 position) {
        gameObject.transform.position = position;
    }

    private void OnBecameVisible() {
        state = UFOState.Moving;
    }

    private void OnBecameInvisible() {
        state = UFOState.Stop;
    }
}