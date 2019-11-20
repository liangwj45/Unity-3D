using System.Collections.Generic;
using UnityEngine;

public class MainSceneController :MonoBehaviour, ISceneController {
    private GameActionManager actionManager;
    private GameGUI gameGUI;
    private GameModel model;
    private GameObject player;
    private Dictionary<int, GameObject> props;
    private float move_speed = 4f;
    private float rotate_speed = 50f;

    void Awake() {
        actionManager = gameObject.AddComponent<GameActionManager>();
        gameGUI = gameObject.AddComponent<GameGUI>();
        gameGUI.onPressRestartButton += delegate {
            Restart();
            model.Restart();
        };
        model = GameModel.GetInstance();
        model.onReFresh += delegate {
            gameGUI.lose = model.lose;
            gameGUI.score = model.score;
        };
        model.onGameover += delegate {
            actionManager.StopAll();
            player.GetComponent<Animator>().SetTrigger("dead");
            foreach (var each in props) {
                each.Value.GetComponent<Animator>().SetBool("run", false);
            }
        };
        Director.GetInstance().OnSceneWake(this);
    }

    void Start() {
        actionManager.AddPropMoves(5);
        Restart();
    }

    void Restart() {
        player.GetComponent<Animator>().Play("New State");
        player.transform.position = new Vector3(3.5f, 0, -2.5f);
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        PropFactory.GetInstance().ResetAll();
        foreach (var each in props) {
            each.Value.GetComponent<Animator>().SetBool("run", true);
        }
        SetPlayerArea(5);
        actionManager.StartAll();
    }

    void Update() {
        if (!model.lose) {
            PlayerMove();
        }
    }

    public void LoadResources() {
        Map map = new Map();
        map.LoadMap();
        player = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        player.transform.position = new Vector3(3.5f, 0, -2.5f);
        props = PropFactory.GetInstance().GetProps();
        actionManager.player = player;
        actionManager.props = props;
    }

    public void PlayerMove() {
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        if (translationX != 0 || translationZ != 0) {
            player.GetComponent<Animator>().SetBool("run", true);
            player.transform.Translate(0, 0, translationZ * move_speed * Time.deltaTime);
            player.transform.Rotate(0, translationX * rotate_speed * Time.deltaTime, 0);
            if (player.transform.localEulerAngles.x != 0 || player.transform.localEulerAngles.z != 0) {
                player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
            }
            if (player.transform.position.y != 0) {
                player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            }
        } else {
            player.GetComponent<Animator>().SetBool("run", false);
        }
    }

    public void SetPlayerArea(int sign) {
        actionManager.SetPlayerArea(sign);
    }
}
