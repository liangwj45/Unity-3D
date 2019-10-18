using System.Collections.Generic;
using UnityEngine;

public class Ruler {
    private static int[] UFOCount = { 1, 2, 3, 3, 4, 4, 4, 5, 5, 6 };
    private static int[] score = { 1, 2, 3 };
    private static string[] colors = { "Red", "Green", "Blue" };
    private UFOFactory factory;
    private System.Random random;
    public ActionManager actionManager;

    public Ruler() {
        this.factory = UFOFactory.GetInstance();
        this.random = new System.Random();
    }

    public int GetUFOCount(int round) {
        return UFOCount[round - 1];
    }

    public List<GameObject> GetUFOs(int round, ActionType type) {
        List<GameObject> ufos = new List<GameObject>();
        var count = GetUFOCount(round);

        for (int i = 0; i < count; ++i) {
            var index = random.Next(3);
            var obj = factory.GetUFO(colors[index], type);
            obj.transform.position = GetRandomPosition();

            var ufo = obj.GetComponent<UFO>();
            ufo.score = GetScore(round, index);
            ufo.visible = true;

            var speed = GetSpeed(round);
            var direction = GetRandomDirection(type);
            if (type == ActionType.Kinematics) {
                KinematicsAction action = KinematicsAction.GetAction(direction, speed);
                actionManager.AddAction(obj, action);
            } else {
                PhysicsAction action = PhysicsAction.GetAction(direction, speed);
                actionManager.AddAction(obj, action);
            }

            ufos.Add(obj);
        }
        return ufos;
    }

    public int GetSubScore(int round) {
        return 10;
    }

    public int GetScore(int round, int index) {
        return score[index];
    }

    private float GetSpeed(int round) {
        return round * 0.01f + 0.5f;
    }

    private Vector3 GetRandomDirection(ActionType type) {
        int s1 = random.Next(2) < 1 ? -1 : 1;
        int s2 = type == ActionType.Physics ? 0 : random.Next(2) < 1 ? -1 : 1;
        int s3 = 0;  // random.Next(2) < 1 ? -1 : 1;
        return new Vector3(RandomFloat(10) * s1, RandomFloat(10) * s2, RandomFloat(10) * s3);
    }

    private Vector3 GetRandomPosition() {
        return new Vector3(RandomFloat(1), RandomFloat(4), RandomFloat(1));
    }

    private float RandomFloat(int n) {
        return random.Next(n * 10) / 10.0f;
    }
}
