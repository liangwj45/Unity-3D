using UnityEngine;

public class PlayerCollider :MonoBehaviour {
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            GameModel.GetInstance().GameOver();
        }
    }
}
