using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFactory {
    private static UFOFactory factory;
    private List<GameObject> used = new List<GameObject>();
    private List<GameObject> pool = new List<GameObject>();
    private readonly Vector3 invisible = new Vector3(0, -100, 0);

    public static UFOFactory GetInstance() {
        return factory ?? (factory = new UFOFactory());
    }

    public GameObject GetUFO(string color) {
        GameObject obj;
        if (pool.Count == 0) {
            obj = Object.Instantiate(Resources.Load<GameObject>("Prefabs/UFO"), invisible, Quaternion.identity);
        } else {
            obj = pool[0];
            pool.RemoveAt(0);
        }

        Material material = Object.Instantiate(Resources.Load("Materials/" + color)) as Material;
        obj.GetComponent<MeshRenderer>().material = material;
        obj.AddComponent<UFO>();
        used.Add(obj);
        return obj;
    }

    public void Recycle(GameObject obj) {
        var ufo = obj.GetComponent<UFO>();
        ufo.SetPosition(invisible);
        ufo.state = UFOState.Stop;
        used.Remove(obj);
        pool.Add(obj);
    }
}