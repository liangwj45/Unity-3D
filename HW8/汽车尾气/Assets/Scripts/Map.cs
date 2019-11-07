using UnityEngine;

public class Map : MonoBehaviour {
    public static GameObject roadPrefab;
    public static GameObject carPrefab;
    public static GameObject obstaclePrefab;
    public static GameObject wallPrefab;

    private void Awake() {
        roadPrefab = Resources.Load<GameObject>("Prefabs/Road");
        carPrefab = Resources.Load<GameObject>("Prefabs/Car");
        obstaclePrefab = Resources.Load<GameObject>("Prefabs/Obstacle");
        wallPrefab = Resources.Load<GameObject>("Prefabs/Wall");
    }

    private void Start() {
        LoadCar();
        LoadRoad();
        LoadObstacle();
        LoadWall();
    }

    private void LoadCar() {
        GameObject car = Instantiate(carPrefab);
        car.transform.position = new Vector3(-2.5f, 0, 0);
    }

    private void LoadRoad() {
        for (int i = 0; i < 5; i++) {
            GameObject roadL = Instantiate(roadPrefab);
            roadL.transform.position = new Vector3(5 + 10 * i, 0, 0);
            GameObject roadR = Instantiate(roadPrefab);
            roadR.transform.position = new Vector3(-5 - 10 * i, 0, 0);
        }
    }

    private void LoadObstacle() {
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 2; j++) {
                GameObject obstacle1 = Instantiate(obstaclePrefab);
                obstacle1.transform.position = new Vector3(5 + 10 * i, 1, 50 + 100 * j);
                GameObject obstacle2 = Instantiate(obstaclePrefab);
                obstacle2.transform.position = new Vector3(-5 - 10 * i, 1, -50 - 100 * j);
                GameObject obstacle3 = Instantiate(obstaclePrefab);
                obstacle3.transform.position = new Vector3(-5 - 10 * i, 1, 50 + 100 * j);
                GameObject obstacle4 = Instantiate(obstaclePrefab);
                obstacle4.transform.position = new Vector3(5 + 10 * i, 1, -50 - 100 * j);
            }
        }
    }

    private void LoadWall() {
        GameObject wallL = Instantiate(wallPrefab);
        wallL.transform.position = new Vector3(-49.5f, 1, 0);
        wallL.transform.localScale = new Vector3(1, 2, 500);
        GameObject wallR = Instantiate(wallPrefab);
        wallR.transform.position = new Vector3(49.5f, 1, 0);
        wallR.transform.localScale = new Vector3(1, 2, 500);
        GameObject wallU = Instantiate(wallPrefab);
        wallU.transform.position = new Vector3(0, 1, 250);
        wallU.transform.localScale = new Vector3(100, 2, 1);
        GameObject wallD = Instantiate(wallPrefab);
        wallD.transform.position = new Vector3(0, 1, -250);
        wallD.transform.localScale = new Vector3(100, 2, 1);
    }
}