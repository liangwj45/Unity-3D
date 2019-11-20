using UnityEngine;


public class ClickGUI :MonoBehaviour {
    public IUserActionController userActionController;
    public Character character;

    void Start() {
        userActionController = SSDirector.getInstance().currentScenceController as IUserActionController;
    }

    public void OnMouseDown() {
        if (gameObject.name == "boat") {
            userActionController.MoveBoat();
        } else {
            userActionController.UpDownBoat(character);
        }
    }
}
