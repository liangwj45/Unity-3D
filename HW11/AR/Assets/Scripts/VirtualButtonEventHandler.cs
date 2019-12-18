using UnityEngine;
using Vuforia;

public class VirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler {
    public VirtualButtonBehaviour[] vbs;
    public Animator animator;

    void Start() {
        vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; i++) {
            vbs[i].RegisterEventHandler(this);
            Debug.Log(vbs[i].VirtualButtonName);
        }
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb) {
        Debug.Log("Pressed");
        switch (vb.VirtualButtonName) {
        case "lvb":
            animator.SetTrigger("Walk");
            animator.ResetTrigger("Jump");
            break;
        default:
            animator.SetTrigger("Jump");
            animator.ResetTrigger("Walk");
            break;
        }
        animator.ResetTrigger("Idle");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb) {
        Debug.Log("Released");
        animator.SetTrigger("Idle");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Walk");
    }
}
