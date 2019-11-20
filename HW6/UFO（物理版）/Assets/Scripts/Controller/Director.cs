﻿using UnityEngine;

public class Director :System.Object {
    private static Director instance;
    public ISceneController currentSceneController { get; set; }
    public bool running { get; set; }
    public int fps {
        get { return Application.targetFrameRate; }
        set { Application.targetFrameRate = value; }
    }

    public static Director GetInstance() {
        return instance ?? (instance = new Director());
    }

    public void OnSceneWake(ISceneController controller) {
        currentSceneController = controller;
        controller.LoadResources();
    }
}
