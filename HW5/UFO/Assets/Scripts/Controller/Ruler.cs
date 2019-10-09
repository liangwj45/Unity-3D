using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ruler {
    private readonly int currentRound;
    private System.Random random;
    private static int[] UFOCount = { 1, 2, 2, 3, 3, 4, 4, 5, 5, 5 };
    private static int[] score = { 1, 2, 3 };
    private static string[] colors = { "Red", "Green", "Blue" };

    public Ruler(int currentRound) {
        this.currentRound = currentRound;
        this.random = new System.Random();
    }

    public int GetUFOCount() {
        return UFOCount[currentRound - 1];
    }

    public List<GameObject> GetUFOs() {
        List<GameObject> ufos = new List<GameObject>();
        var index = random.Next(3);
        var color = colors[index];
        var count = GetUFOCount();
        for (int i = 0; i < count; ++i) {
            Vector3 position = GetRandomPosition();
            var obj = UFOFactory.GetInstance().GetUFO(color);
            var ufo = obj.GetComponent<UFO>();
            ufo.SetPosition(position);
            ufo.score = score[index] * (currentRound + 1);
            ufo.speed = currentRound * 0.01f + 0.5f;
            ufo.state = UFOState.Moving;
            ufo.direction = GetRandomDirection();
            ufos.Add(obj);
        }

        return ufos;
    }

    private Vector3 GetRandomDirection() {
        int s1 = random.Next(2) < 1 ? -1 : 1;
        int s2 = random.Next(2) < 1 ? -1 : 1;
        int s3 = random.Next(2) < 1 ? -1 : 1;
        return new Vector3(RandomFloat(10) * s1, RandomFloat(10) * s2, RandomFloat(10) * s3);
    }

    private Vector3 GetRandomPosition() {
        return new Vector3(RandomFloat(1), RandomFloat(4), RandomFloat(1));
    }

    private float RandomFloat(int n) {
        return random.Next(n * 10) / 10.0f;
    }
}

