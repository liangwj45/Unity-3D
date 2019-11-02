using UnityEngine;

public class AreaCollider :MonoBehaviour {
    MainSceneController sceneController;
    public int sign = 0;

    void Start() {
        sceneController = Director.GetInstance().currentSceneController as MainSceneController;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            sceneController.SetPlayerArea(sign);
            GameModel.GetInstance().PlayerEscape();
        }
    }
}
