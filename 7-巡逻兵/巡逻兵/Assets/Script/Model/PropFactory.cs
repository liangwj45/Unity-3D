using System.Collections.Generic;
using UnityEngine;

public class PropFactory {
    private static PropFactory instance = new PropFactory();
    private Dictionary<int, GameObject> props = new Dictionary<int, GameObject>();
    private GameObject propPrefabs = Resources.Load<GameObject>("Prefabs/Prop");
    private int props_count = 9;

    private PropFactory() { }

    public static PropFactory GetInstance() {
        return instance ?? (instance = new PropFactory());
    }

    public Dictionary<int, GameObject> GetProps() {
        if (props.Count == props_count) return props;
        int index = 0;
        for (int i = -10; i <= 10; i += 10) {
            for (int j = -10; j <= 10; j += 10) {
                GameObject prop = Object.Instantiate(propPrefabs);
                prop.transform.position = new Vector3(i, 0, j);
                prop.name = "Prop" + (++index);
                prop.AddComponent<PlayerCollider>();
                props.Add(index, prop);
            }
        }
        return props;
    }

    public void ResetAll() {
        if (props.Count < props_count) GetProps();
        for (int i = -10, index = 0; i <= 10; i += 10) {
            for (int j = -10; j <= 10; j += 10) {
                props[++index].transform.position = new Vector3(i, 0, j);
            }
        }
    }
}
