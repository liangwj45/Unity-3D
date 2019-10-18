using UnityEngine;

public class UFO :MonoBehaviour {
    public int score;
    public bool visible = false;

    private void OnBecameVisible() {
        visible = true;
    }

    private void OnBecameInvisible() {
        visible = false;
    }
}
