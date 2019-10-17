using UnityEngine;

public class Main :MonoBehaviour {
    public bool pressed = false;
    void Start() {
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("点击鼠标左键");
            pressed = !pressed;
            GetComponent<Animator>().SetBool("pressed", pressed);
        }
    }
}
