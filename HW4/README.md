# HW4：牧师与魔鬼（动作分离版）

视频链接：<https://www.bilibili.com/video/av70393139/>

项目链接：

博客链接：

## 程序要求

- 将游戏中对象的动作分离出来，实现动作和物体属性的分离
- 添加裁判类，在游戏状态发生改变时通知场景控制器

## 游戏截图

![preist](.\img\preist.png)

## 编写程序

### 动作基类（SSAction）

```c#
public class SSAction :ScriptableObject {
    public bool enable = true;
    public bool destroy = false;

    public GameObject gameObject { get; set; }
    public Transform transform { get; set; }
    public ISSActionCallback callback { get; set; }

    protected SSAction() { }

    public virtual void Start() {
        throw new System.NotImplementedException();
    }

    public virtual void Update() {
        throw new System.NotImplementedException();
    }
}
```

### 简单动作实现（CCMoveToAction）

```c#
public class CCMoveToAction :SSAction {
    public Vector3 target;
    public float speed;

    public static CCMoveToAction GetAction(Vector3 target, float speed) {
        CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }

    public override void Update() {
        this.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target) {
            destroy = true;
            callback.ISSActionCallback(this);
        }
    }

    public override void Start() {

    }
}
```

### 顺序动作组合类实现（CCSequenceAction）

```c#
public class CCSequenceAction :SSAction, ISSActionCallback {
    public List<SSAction> sequence;
    public int repeat = 1; // 1->only do it for once, -1->repeat forever
    public int start = 0;

    public static CCSequenceAction GetAction(int repeat, int start, List<SSAction> sequence) {
        CCSequenceAction action = ScriptableObject.CreateInstance<CCSequenceAction>();
        action.sequence = sequence;
        action.repeat = repeat;
        action.start = start;
        return action;
    }


    public override void Update() {
        if (sequence.Count == 0) return;
        if (start < sequence.Count) {
            sequence[start].Update();
        }
    }

    public void ISSActionCallback(SSAction source) {
        source.destroy = false;
        this.start++;
        if (this.start >= sequence.Count) {
            this.start = 0;
            if (repeat > 0) repeat--;
            if (repeat == 0) {
                this.destroy = true;
                this.callback.ISSActionCallback(this);
            }
        }
    }

    public override void Start() {
        foreach (SSAction action in sequence) {
            action.gameObject = this.gameObject;
            action.transform = this.transform;
            action.callback = this;
            action.Start();
        }
    }

    void OnDestroy() {
    }
}
```

### 动作事件接口定义（ISSActionCallback）

```c#
public enum SSActionEventType :int { Started, Completed }
public interface ISSActionCallback {
    void ISSActionCallback(SSAction source);
}
```

### 动作管理基类（SSActionManager）

```c#
public class SSActionManager :MonoBehaviour {
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> add_list = new List<SSAction>();
    private List<int> delete_list = new List<int>();

    protected void Update() {
        foreach (SSAction ac in add_list) {
            actions[ac.GetInstanceID()] = ac;
        }
        add_list.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions) {
            SSAction ac = kv.Value;
            if (ac.destroy) {
                delete_list.Add(ac.GetInstanceID());
            } else if (ac.enable) {
                ac.Update();
            }
        }

        foreach (int key in delete_list) {
            SSAction ac = actions[key];
            actions.Remove(key);
            // DestroyObject(ac);
        }
        delete_list.Clear();
    }

    public void AddAction(GameObject gameObject, SSAction action, ISSActionCallback ICallBack) {
        action.gameObject = gameObject;
        action.transform = gameObject.transform;
        action.callback = ICallBack;
        add_list.Add(action);
        action.Start();
    }
}
```

### 动作管理实现类（ActionManager）

```c#
public class ActionManager :SSActionManager, ISSActionCallback {
    public SSActionEventType complete = SSActionEventType.Completed;

    public void BoatMove(Boat boat) {
        complete = SSActionEventType.Started;
        Vector3 destination = boat.gameObject.transform.position;
        destination.x = -destination.x;
        CCMoveToAction action = CCMoveToAction.GetAction(destination, boat.speed);
        AddAction(boat.gameObject, action, this);
        boat.state = boat.state == BoatState.Left ? BoatState.Right : BoatState.Left;
    }

    public void UpDownBoat(Character character, Vector3 destination) {
        complete = SSActionEventType.Started;
        Vector3 position = character.gameObject.transform.position;
        Vector3 middle = position;
        if (destination.y > position.y) {
            middle.y = destination.y;
        } else {
            middle.x = destination.x;
        }
        SSAction action1 = CCMoveToAction.GetAction(middle, character.speed);
        SSAction action2 = CCMoveToAction.GetAction(destination, character.speed);
        SSAction sequence = CCSequenceAction.GetAction(1, 0, new List<SSAction> { action1, action2 });
        this.AddAction(character.gameObject, sequence, this);
    }

    public void ISSActionCallback(SSAction source) {
        complete = SSActionEventType.Completed;
    }
}
```

### 裁判类（Judger）

```c#
public class Judger :MonoBehaviour {
    public Character[] preists;
    public Character[] devils;
    public Boat boat;
    public GameState gameState;
    public ISceneController sceneController;

    public void Start() {
        gameState = GameState.Continue;
    }

    public void Update() {
        GameState state = Check();

        // Debug.Log(gameState);
        if (gameState != state) {
            sceneController.UpdateGameState(state);
            gameState = state;
        }
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
```

### 其他修改

修改原有的Character类和Boat类，去除原来的Move对象，并修改场景控制类，修改对象移动以及处理游戏状态改变的部分代码。

