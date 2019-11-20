using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolution :MonoBehaviour {
    public GameObject center;
    public Vector3 plane = new Vector3(0, 1, 0);
    public float speed = 50;
    void Update() {
        this.transform.RotateAround(center.transform.position, plane, speed * Time.deltaTime);
    }
}
