# 牧师与魔鬼

视频链接：<https://www.bilibili.com/video/av68569408/>

项目链接：<https://github.com/liangwj45/Unity-3D/tree/master/HW3/牧师与魔鬼>

博客链接：<https://liangwj45.github.io/2019/09/22/Unity3D制作牧师与魔鬼游戏/>

## 程序要求

- MVC 结构
- 游戏中对象做成预制
- 只能有摄像机和 Empty 对象，其他对象由代码生成，不能使用 Find，SendMessage 方法。
- 使用 C# 集合类型有效组织对象
- 船未靠岸，牧师与魔鬼上下船运动中，均不能接受用户事件！

## 游戏对象

牧师、魔鬼、船

## 游戏规则

- 帮助3个牧师（方块）和3个魔鬼（圆球）渡河。
- 船上最多可以载2名游戏角色。
- 船上有游戏对象时才可以移动。
- 当有一侧岸的魔鬼数多余牧师数时（包括船上的魔鬼和牧师），魔鬼就会失去控制，吃掉牧师（如果这一侧没有牧师则不会失败），游戏失败。
- 当所有游戏角色都到达对岸时，游戏胜利。

## 玩家动作表

| 动作         | 发生条件                               |
| ------------ | -------------------------------------- |
| 上船         | 船上有空位，点击对应岸边想要上船的角色 |
| 下船         | 点击船上想要下船的角色                 |
| 开船         | 点击船身，船上有人                     |
| 重新开始游戏 | 点击重新开始按钮                       |

## 界面设计

构建预设对象，为游戏中每个对象找到相应的贴纸，设计游戏对象大小和可能出现的位置。

## 编写程序

游戏使用 MVC 架构来实现。

### View 层

游戏界面控制类：

```c#
using UnityEngine;

namespace PreistDevil {
    public enum GameState :uint {
        Continue, Win, Gameover
    }
    public class GameGUI :MonoBehaviour {
        public IUserActionController userActionController;
        public GameState gameState;

        public void Start() {
            gameState = GameState.Continue;
            userActionController = SSDirector.getInstance().currentScenceController as IUserActionController;
        }

        public void Restart() {
            gameState = GameState.Continue;
        }

        public void OnGUI() {
            if (gameState == GameState.Continue) return;
            string msg = gameState == GameState.Gameover ? "Gameover!" : "Win!";
            GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 + 15, 100, 50), msg);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 60, 140, 70), "Restart")) {
                userActionController.Restart();
            }
        }
    }
}
```

用户交互类：

```c#
using UnityEngine;

namespace PreistDevil {
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
}

```

### Contorl 层

导演类：

```c#
using UnityEngine;

namespace PreistDevil {
    public class SSDirector {

        private static SSDirector _instance;

        public ISceneController currentScenceController { get; set; }
        public bool running { get; set; }

        public static SSDirector getInstance() {
            if (_instance == null) {
                _instance = new SSDirector();
            }
            return _instance;
        }

        public int getFPS() {
            return Application.targetFrameRate;
        }

        public void setFPS(int fps) {
            Application.targetFrameRate = fps;
        }
    }
}
```

场景接口：

```c#
namespace PreistDevil {
    public interface ISceneController {
        void LoadResources();
    }
}
```

用户操作接口：

```c#
namespace PreistDevil {
    public interface IUserActionController {
        void MoveBoat();
        void UpDownBoat(Character character);
        void Restart();
    }
}
```

场景控制类：

```c#
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
```

对象移动控制类：

```c#
using UnityEngine;

namespace PreistDevil {
    public enum MoveState :uint {
        Stoped, MovingToMiddle, MovingToDestination
    }
    public class Move :MonoBehaviour {
        public float speed = 20f;
        public Vector3 destination;
        public Vector3 middle;
        public MoveState state = MoveState.Stoped;

        void Update() {
            if (state == MoveState.Stoped) return;
            if (state == MoveState.MovingToMiddle) {
                if (transform.position == middle) {
                    state = MoveState.MovingToDestination;
                } else {
                    transform.position = Vector3.MoveTowards(transform.position, middle, speed * Time.deltaTime);
                }
            } else {
                if (transform.position == destination) {
                    state = MoveState.Stoped;
                } else {
                    transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                }
            }
        }

        public void SetDestination(Vector3 destination) {
            this.destination = middle = destination;
            if (transform.position.y < destination.y) {
                middle.x = transform.position.x;
                state = MoveState.MovingToMiddle;
            } else if (transform.position.y > destination.y) {
                middle.y = transform.position.y;
                state = MoveState.MovingToMiddle;
            } else {
                state = MoveState.MovingToDestination;
            }
        }
    }
}
```

### Model 层

对象接口：

```c#
using UnityEngine;

namespace PreistDevil {
    public interface IObject {
        void SetPosition(Vector3 position);
        void SetScale(Vector3 scale);
    }
}
```

河流类：

```c#
using UnityEngine;

namespace PreistDevil {
    public class Water :IObject {
        public GameObject gameObject;
        public Water(GameObject obj) {
            gameObject = obj;
        }

        public void SetPosition(Vector3 position) {
            gameObject.transform.position = position;
        }

        public void SetScale(Vector3 scale) {
            gameObject.transform.localScale = scale;
        }

        public void Init() { }
    }
}
```

河岸类：

```c#
using UnityEngine;

namespace PreistDevil {
    public class Coast :IObject {
        public GameObject gameObject;
        public Vector3[] positions;
        public bool[] occupied;
        public int state;

        public Coast(GameObject obj, string side) {
            gameObject = obj;
            state = side == "left" ? -1 : 1;
            positions = new Vector3[6];
            occupied = new bool[6];
            for (int i = 0; i < 6; ++i) {
                positions[i] = new Vector3(state * (3.4f + 0.6f * i), 1.2f, 0);
            }
        }

        public void Init(bool occupied) {
            for (int i = 0; i < 6; ++i) {
                this.occupied[i] = occupied;
            }
        }

        public Vector3 GetEmptyPosition() {
            for (int i = 0; i < 6; ++i) {
                if (!occupied[i]) {
                    occupied[i] = true;
                    return positions[i];
                }
            }
            return new Vector3(0, 0, 0);
        }

        public bool ReleasePosition(Vector3 position) {
            for (int i = 0; i < 6; ++i) {
                if (positions[i] == position) {
                    occupied[i] = false;
                    return true;
                }
            }
            return false;
        }

        public void SetPosition(Vector3 position) {
            gameObject.transform.position = position;
        }

        public void SetScale(Vector3 scale) {
            gameObject.transform.localScale = scale;
        }
    }
}
```

船类：

```c#
using UnityEngine;

namespace PreistDevil {
    public enum BoatState {
        Left = -1, Right = 1
    }

    public class Boat :IObject {
        public GameObject gameObject;
        public Vector3 position;
        public bool[] occupied;
        public BoatState state;
        public Move move;

        public Boat(GameObject obj, string name) {
            gameObject = obj;
            gameObject.name = name;
            occupied = new bool[2];
        }

        public void Init() {
            occupied[0] = occupied[1] = false;
            SetPosition(position);
            state = BoatState.Right;
        }

        public Vector3 GetEmptyPosition() {
            for (int i = 0; i < 2; ++i) {
                if (!occupied[i]) {
                    occupied[i] = true;
                    return new Vector3(gameObject.transform.position.x - 0.3f + 0.6f * i, 0.9f, 0);
                }
            }
            // TODO: throw error
            return new Vector3(0, 0, 0);
        }

        public void ReleasePosition(Vector3 position) {
            if (position.x < gameObject.transform.position.x) {
                occupied[0] = false;
            } else {
                occupied[1] = false;
            }
        }

        public bool Full() {
            return occupied[0] && occupied[1];
        }

        public bool HasPassager() {
            return occupied[0] || occupied[1];
        }

        public void SetPosition(Vector3 position) {
            gameObject.transform.position = position;
            if (this.position == new Vector3(0, 0, 0)) {
                this.position = position;
            }
        }

        public void SetScale(Vector3 scale) {
            gameObject.transform.localScale = scale;
        }
    }
}
```

角色类：

```c#
using UnityEngine;

namespace PreistDevil {
    public enum CharacterState : uint {
        OnBoat, OnCoastL, OnCoastR
    }

    public class Character :IObject {
        public GameObject gameObject;
        public Vector3 position;
        public Move move;
        public ClickGUI clickGUI;
        public CharacterState state;

        public Character(GameObject obj, string name) {
            gameObject = obj;
            gameObject.name = name;
            state = CharacterState.OnCoastR;
        }

        public void SetPosition(Vector3 position) {
            gameObject.transform.position = position;
            if (this.position == new Vector3(0, 0, 0)) {
                this.position = position;
            }
        }

        public void SetScale(Vector3 scale) {
            gameObject.transform.localScale = scale;
        }

        public void Init() {
            gameObject.transform.parent = null;
            state = CharacterState.OnCoastR;
            SetPosition(position);
        }

        public void MoveToPosition(Vector3 destination) {
            move.SetDestination(destination);
        }
    }
}
```

### 运行程序

![](./img/preist.png)

