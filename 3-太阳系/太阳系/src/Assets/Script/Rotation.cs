using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation :MonoBehaviour {
    public Vector3 plane = new Vector3(0, 1, 0);
    public int speed = 10;
    void Update() {
        this.transform.RotateAround(this.transform.position, plane, speed * Time.deltaTime);
    }
}
