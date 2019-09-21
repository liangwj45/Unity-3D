using UnityEngine;

namespace PreistDevil {
    public class MainSceneController :MonoBehaviour, ISceneController, IUserActionController {
        public Water water;
        public Coast coastL;
        public Coast coastR;
        public Character[] preists;
        public Character[] devils;
        public Boat boat;
        public GameGUI gameGUI;

        void Awake() {
            gameGUI = gameObject.AddComponent<GameGUI>() as GameGUI;
            preists = new Character[3];
            devils = new Character[3];
            SSDirector.getInstance().currentScenceController = this;
            LoadResources();
        }

        public void LoadResources() {
            water = new Water(Instantiate(Resources.Load("Prefabs/Water")) as GameObject);
            water.SetPosition(new Vector3(0, 0.2f, 0));
            water.SetScale(new Vector3(5.6f, 0.4f, 1));
            water.Init();

            coastL = new Coast(Instantiate(Resources.Load("Prefabs/Coast")) as GameObject, "left");
            coastL.SetPosition(new Vector3(-4.7f, 0.5f, 0));
            coastL.SetScale(new Vector3(3.8f, 1, 1));
            coastL.Init();

            coastR = new Coast(Instantiate(Resources.Load("Prefabs/Coast")) as GameObject, "right");
            coastR.SetPosition(new Vector3(4.7f, 0.5f, 0));
            coastR.SetScale(new Vector3(3.8f, 1, 1));
            coastR.Init();

            boat = new Boat(Instantiate(Resources.Load("Prefabs/Boat")) as GameObject, "boat");
            boat.gameObject.AddComponent(typeof(Move));
            boat.gameObject.AddComponent(typeof(ClickGUI));
            boat.SetPosition(new Vector3(2.1f, 0.55f, 0));
            boat.SetScale(new Vector3(1.4f, 0.3f, 1));
            boat.Init();

            for (int i = 0;i < 3; ++i) {
                preists[i] = new Character(Instantiate(Resources.Load("Prefabs/Preist")) as GameObject, "preist");
                preists[i].gameObject.AddComponent(typeof(Move));
                preists[i].gameObject.AddComponent(typeof(ClickGUI));
                preists[i].SetPosition(coastR.GetEmptyPosition());
                preists[i].SetScale(new Vector3(0.4f, 0.4f, 0.4f));
            }

            for (int i = 0; i < 3; ++i) {
                devils[i] = new Character(Instantiate(Resources.Load("Prefabs/Devil")) as GameObject, "devil");
                devils[i].gameObject.AddComponent(typeof(Move));
                devils[i].gameObject.AddComponent(typeof(ClickGUI));
                devils[i].SetPosition(coastR.GetEmptyPosition());
                devils[i].SetScale(new Vector3(0.4f, 0.4f, 0.4f));
            }
        }

        public void UpDownBoat(GameObject obj) {
            gameGUI.gameState = Check();
        }

        public void MoveBoat() {

        }

        public void Restart() {
            boat.Init();
            for (int i = 0; i< 3; ++i) {
                preists[i].Init();
                devils[i].Init();
            }
            gameGUI.Restart();
        }

        public int Check() {
            return 0;
        }
    }
}