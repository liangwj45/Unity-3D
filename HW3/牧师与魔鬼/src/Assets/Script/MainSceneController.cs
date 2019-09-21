using UnityEngine;

namespace PreistDevil {
    public class MainSceneController :MonoBehaviour, ISceneController, IUserAction {
        public Water water;
        public Coast coastL;
        public Coast coastR;
        public Character[] preists;
        public Character[] devils;
        public Boat boat;
        public GameGUI gameGUI;
        void Awake() {
            gameGUI = new GameGUI();
            preists = new Character[3];
            devils = new Character[3];
            SSDirector director = SSDirector.getInstance();
            director.currentScenceController = this;
            director.currentScenceController.LoadResources();
        }

        public void LoadResources() {
            water = new Water(Instantiate(Resources.Load("Prefabs/Water")) as GameObject);
            coastL = new Coast(Instantiate(Resources.Load("Prefabs/Coast")) as GameObject, "left");
            coastR = new Coast(Instantiate(Resources.Load("Prefabs/Coast")) as GameObject, "right");
            boat = new Boat(Instantiate(Resources.Load("Prefabs/Boat")) as GameObject, ObjectType.BOAT);
            for (int i = 0;i < 3; ++i) {
                preists[i] = new Character(Instantiate(Resources.Load("Prefabs/Preist")) as GameObject, ObjectType.PREIST);
                preists[i].SetPosition(coastR.GetEmptyPosition());
            }
            for (int i = 0; i < 3; ++i) {
                devils[i] = new Character(Instantiate(Resources.Load("Prefabs/Devil")) as GameObject, ObjectType.DEVIL);
                devils[i].SetPosition(coastR.GetEmptyPosition());
            }
        }

        public void ObjectIsClicked(IObject obj) {
            if (obj.GetObjectType() == ObjectType.DEVIL || obj.GetObjectType() == ObjectType.PREIST) {

            } else if (obj.GetObjectType() == ObjectType.BOAT) {
                if (boat.HasPassager()) {
                    MoveBoat();
                }
            }
            gameGUI.state = Check();
        }

        public void MoveBoat() {

        }

        public void Restart() {

        }

        public int Check() {
            return 0;
        }
    }
}