# HW3：空间与运动

## 1、简答并用程序验证

- 游戏对象运动的本质是什么？

  使用矩阵变换（平移、旋转、缩放）改变游戏对象的空间属性。

  ```c#
  // 平移
  public class Translate :MonoBehaviour {
      public int speed = 2;
      void Update() {
  		this.transform.Translate(speed * Vector3.forward * Time.deltaTime);
      }
  }
  // 旋转
  public class Rotating :MonoBehaviour {
      public float x, y, z;
      void Update() {
          this.transform.Rotate(x, y, z, Space.Self);
          //this.transform.Rotate(x, y, z, Space.World);
      }
  }
  // 缩放
  public class Zooming :MonoBehaviour {
      void Update() {
          float x = Input.GetAxis("Horizontal") * Time.deltaTime;
          float z = Input.GetAxis("Vertical") * Time.deltaTime;
          this.transform.localScale += new Vector3(x, 0, z);
      }
  }
  ```

- 请用三种方法以上方法，实现物体的抛物线运动。（如，修改 Transform 属性，使用向量 Vector3 的方法…）

  - 方法一：直接改变物体的 Position：$Y= X^2$

    ```c#
    public class Move :MonoBehaviour {
        public int speed = 2;
        void Start() {
            transform.position = new Vector3(-4, 16, 0);
        }
        void Update() {
            transform.position += speed * Vector3.right * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.x * transform.position.x, 0);
        }
    }
    ```

  - 方法二：通过给定 X 轴方向的移动速度及 Y 轴方向上的初始加速度，及初始速度。

    ```c#
    public class Move :MonoBehaviour {
        public float xspeed = 3;
        public float yspeed = -3;
        public int a = 1;
        void Update() {
            yspeed += a * Time.deltaTime;
            transform.Translate(xspeed * Vector3.right * Time.deltaTime);
            transform.Translate(yspeed * Vector3.up * Time.deltaTime);
        }
    }
    ```

  - 方法三：通过设置刚体属性，并给予物体重力加速度和初始速度。

    ```c#
    public class Move :MonoBehaviour {
        void Start() {
            transform.position = new Vector3(-4, 16, 0);
        }
        void Update() {
            Rigidbody rbd = this.gameObject.AddComponent<Rigidbody>();
            rbd.useGravity = true;
            rbd.velocity = Vector3.right * 5;
        }
    }
    ```

## 2、编程实践

### 太阳系

项目地址：<https://github.com/liangwj45/Unity-3D/tree/master/HW3/太阳系>

视频链接：<https://www.bilibili.com/video/av68259514>

博客链接：<https://liangwj45.github.io/2019/09/21/Unity3D制作太阳系动画/>

![](./太阳系/img/solar.png)

### 牧师与魔鬼

项目地址：<https://github.com/liangwj45/Unity-3D/tree/master/HW3/牧师与魔鬼>

视频链接：<https://www.bilibili.com/video/av68569408/>

博客链接：<https://liangwj45.github.io/2019/09/22/Unity3D制作牧师与魔鬼游戏/>

![](./牧师与魔鬼/img/preist.png)
