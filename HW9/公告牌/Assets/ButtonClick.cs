using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {

    public Text text;  // 绑定一个Text对象
    private int frame = 20;
    private float text_width = 0;

    // 初始化工作
    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(ActionOnClick);
        text_width = text.rectTransform.sizeDelta.x;
    }

    // 点击按钮逻辑
    void ActionOnClick() {
        // 根据文本框状态执行相应动画
        if (text.gameObject.activeSelf) {
            StartCoroutine(CloseText());
        } else {
            StartCoroutine(OpenText());
        }
    }

    // 文本框关闭动画
    private IEnumerator CloseText() {
        // 设定旋转的初始值和旋转速度
        float rotation_x = 0;
        float rotate_speed = 90f / frame;

        // 设定文本框高度初始值和缩放速度
        float text_height = 80f;
        float scale_speedh = text_height / frame;

        // 执行动画
        for (int i = 0; i < frame; i++) {
            rotation_x -= rotate_speed;
            text_height -= scale_speedh;
            // 设置文本框的旋转角度和高度
            text.transform.rotation = Quaternion.Euler(rotation_x, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text_width, text_height);
            // 动画结束
            if (i == frame - 1) {
                text.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    // 文本框打开动画
    private IEnumerator OpenText() {
        // 设定旋转的初始值和旋转速度
        float rotation_x = -90;
        float rotate_speed = 90f / frame;

        // 设定文本框高度初始值和缩放速度
        float text_height = 0;
        float scale_speedh = 80f / frame;

        // 执行动画
        for (int i = 0; i < frame; i++) {
            rotation_x += rotate_speed;
            text_height += scale_speedh;
            // 设置文本框的旋转角度和高度
            text.transform.rotation = Quaternion.Euler(rotation_x, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text_width, text_height);
            // 动画结束
            if (i == 0) {
                text.gameObject.SetActive(true);
            }
            yield return null;
        }
    }
}
