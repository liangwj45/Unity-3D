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
            coastL.Init(false);

            coastR = new Coast(Instantiate(Resources.Load("Prefabs/Coast")) as GameObject, "right");
            coastR.SetPosition(new Vector3(4.7f, 0.5f, 0));
            coastR.SetScale(new Vector3(3.8f, 1, 1));
            coastR.Init(false);

            boat = new Boat(Instantiate(Resources.Load("Prefabs/Boat")) as GameObject, "boat");
            boat.move = boat.gameObject.AddComponent(typeof(Move)) as Move;
            boat.gameObject.AddComponent(typeof(ClickGUI));
            boat.SetPosition(new Vector3(2.1f, 0.55f, 0));
            boat.SetScale(new Vector3(1.4f, 0.3f, 1));
            boat.Init();

            for (int i = 0; i < 3; ++i) {
                preists[i] = new Character(Instantiate(Resources.Load("Prefabs/Preist")) as GameObject, "preist");
                preists[i].move = preists[i].gameObject.AddComponent(typeof(Move)) as Move;
                ClickGUI clickGUI = preists[i].gameObject.AddComponent(typeof(ClickGUI)) as ClickGUI;
                clickGUI.character = preists[i];
                preists[i].clickGUI = clickGUI;
                preists[i].clickGUI.character = preists[i];
                preists[i].SetPosition(coastR.GetEmptyPosition());
                preists[i].SetScale(new Vector3(0.4f, 0.4f, 0.4f));
            }

            for (int i = 0; i < 3; ++i) {
                devils[i] = new Character(Instantiate(Resources.Load("Prefabs/Devil")) as GameObject, "devil");
                devils[i].move = devils[i].gameObject.AddComponent(typeof(Move)) as Move;
                ClickGUI clickGUI = devils[i].gameObject.AddComponent(typeof(ClickGUI)) as ClickGUI;
                clickGUI.character = preists[i];
                devils[i].clickGUI = clickGUI;
                devils[i].clickGUI.character = devils[i];
                devils[i].SetPosition(coastR.GetEmptyPosition());
                devils[i].SetScale(new Vector3(0.4f, 0.4f, 0.4f));
            }
        }

        public void UpDownBoat(Character character) {
            Vector3 position = new Vector3(0, 0, 0);
            if (character.state == CharacterState.OnBoat) {
                boat.ReleasePosition(character.gameObject.transform.position);
                character.gameObject.transform.parent = null;
                if (boat.state == BoatState.Right) {
                    position = coastR.GetEmptyPosition();
                    character.state = CharacterState.OnCoastR;
                } else {
                    position = coastL.GetEmptyPosition();
                    character.state = CharacterState.OnCoastL;
                }
                character.MoveToPosition(position);
            } else {
                if (boat.Full()) return;
                if ((character.state == CharacterState.OnCoastL && boat.state == BoatState.Left) || (character.state == CharacterState.OnCoastR && boat.state == BoatState.Right)) {
                    position = boat.GetEmptyPosition();
                    character.MoveToPosition(position);
                    character.gameObject.transform.parent = boat.gameObject.transform;
                    character.state = CharacterState.OnBoat;
                    if (boat.state == BoatState.Left) coastL.ReleasePosition(character.gameObject.transform.position);
                    else coastR.ReleasePosition(character.gameObject.transform.position);
                }
            }
            gameGUI.gameState = Check();
        }

        public void MoveBoat() {
            if (boat.HasPassager()) {
                boat.state = boat.state == BoatState.Left ? BoatState.Right : BoatState.Left;
                Vector3 destination = boat.gameObject.transform.position;
                destination.x = -destination.x;
                boat.move.SetDestination(destination);
            }
            gameGUI.gameState = Check();
        }

        public void Restart() {
            gameGUI.Restart();
            coastL.Init(false);
            coastR.Init(true);
            for (int i = 0; i < 3; ++i) {
                preists[i].Init();
                devils[i].Init();
            }
            boat.Init();
        }

        public GameState Check() {
            int preist_left = 0, preist_right = 0, devil_left = 0, devil_right = 0, win = 0;
            for (int i = 0; i < 3; ++i) {
                if (preists[i].state == CharacterState.OnCoastL) {
                    preist_left++; win++;
                } else if (preists[i].state == CharacterState.OnBoat && boat.state == BoatState.Left) {
                    preist_left++;
                } else {
                    preist_right++;
                }
                if (devils[i].state == CharacterState.OnCoastL) {
                    devil_left++; win++;
                } else if (devils[i].state == CharacterState.OnBoat && boat.state == BoatState.Left) {
                    devil_left++;
                } else {
                    devil_right++;
                }
            }
            if (win == 6) return GameState.Win;
            if ((preist_left < devil_left && preist_left != 0) || (preist_right < devil_right && preist_right != 0)) return GameState.Gameover;
            return GameState.Continue;
        }
    }
}