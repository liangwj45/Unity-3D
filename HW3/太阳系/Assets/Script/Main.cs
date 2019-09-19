using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main :MonoBehaviour {
    void Start() {
        int i = transform.childCount;
        while (i-- > 0) {
            AddRevolution(transform.GetChild(i), transform.gameObject);
            SetTrailRenderer(transform.GetChild(i));
            AddRotation(transform.GetChild(i));
        }
    }

    private void AddRevolution(Transform trans, GameObject center) {
        GameObject obj = trans.gameObject;
        obj.AddComponent<Revolution>();
        Revolution r = obj.GetComponent<Revolution>();
        r.center = center;
        r.speed = 300 / obj.transform.position.x * Random.Range(1f, 1.5f);
        r.plane = new Vector3(0, Random.Range(0, 1000) / 10f, Random.Range(0, 1000) / 10f);
    }

    private void AddRotation(Transform trans) {
        GameObject obj = trans.gameObject;
        obj.AddComponent<Rotation>();
        Rotation r = obj.GetComponent<Rotation>();
        r.plane = obj.GetComponent<Revolution>().plane;
    }

    private void SetTrailRenderer(Transform trans) {
        GameObject obj = trans.gameObject;
        obj.AddComponent<TrailRenderer>();
        TrailRenderer tr = obj.GetComponent<TrailRenderer>();
        tr.time = 3;
        tr.startWidth = 0.1f;
        tr.endWidth = 0.1f;
        tr.material = new Material(Shader.Find("Sprites/Default"));
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.grey, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        tr.colorGradient = gradient;
    }
}
