using UnityEngine;

public class Map :Object {
    private static GameObject planePrefab = Resources.Load<GameObject>("Prefabs/Plane");
    private static GameObject fencePrefab = Resources.Load<GameObject>("Prefabs/Fence");
    private static GameObject triggerPrefab = Resources.Load<GameObject>("Prefabs/Trigger");

    public void LoadMap() {
        LoadPlane();
        LoadFences();
        LoadWalls();
        LoadTriggers();
    }

    public void LoadPlane() {
        GameObject plane = Instantiate(planePrefab);
        plane.transform.position = new Vector3(0, 0, 0);
    }

    public void LoadFences() {
        int[,] exist = { { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 } };
        for (int i = -13; i <= 14; i++) {
            for (int j = 0; j <= 1; j++) {
                if (exist[j, i + 13] == 1) {
                    GameObject fence1 = Instantiate(fencePrefab);
                    fence1.transform.position = new Vector3(10 * j - 5, 0, (i - 1) * 1.1f);
                    fence1.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                if (exist[j, i + 13] == 1) {
                    GameObject fence2 = Instantiate(fencePrefab);
                    fence2.transform.position = new Vector3(i * 1.1f, 0, 10 * j - 5);
                    fence2.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
    }

    public void LoadWalls() {
        for (int i = -13; i <= 14; i++) {
            for (int j = 0; j <= 1; j++) {
                GameObject fence1 = Instantiate(fencePrefab);
                fence1.transform.position = new Vector3(30 * j - 15, 0, (i - 1) * 1.1f);
                fence1.transform.rotation = Quaternion.Euler(0, 90, 0);
                GameObject fence2 = Instantiate(fencePrefab);
                fence2.transform.position = new Vector3(i * 1.1f, 0, 30 * j - 15);
                fence2.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    public void LoadTriggers() {
        for (int i = -10, index = 0; i <= 10; i += 10) {
            for (int j = -10; j <= 10; j += 10) {
                GameObject trigger = Instantiate(triggerPrefab);
                trigger.transform.position = new Vector3(i, 0, j);
                AreaCollider collider = trigger.AddComponent<AreaCollider>();
                collider.sign = ++index;
                trigger.name = "Trigger" + index;
            }
        }
    }
}
