using System;
using System.Collections.Generic;
using UnityEngine;

public class AISystem {
    public int count = 3;
    public List<State> states = new List<State>();

    public class State {
        public int preist;
        public int devil;
        public State(int preist, int devil) {
            this.preist = preist;
            this.devil = devil;
        }
    }

    public AISystem(int n) {
        count = n;
        // 生成所有可行的状态
        for (int i = 0; i <= count; i++) {
            states.Add(new State(0, i));
            states.Add(new State(count, i));
            if (i != 0 && i != count) {
                states.Add(new State(i, i));
            }
        }
    }

    public bool ContainState(State state) {
        return state.preist == 0 || state.preist == count || state.preist == state.devil;
    }

    public class Node {
        public State state;
        public bool boatOnLeft;
        public int parent;
        public Node(State state, bool boatOnLeft, int parent) {
            this.state = state;
            this.boatOnLeft = boatOnLeft;
            this.parent = parent;
        }
    }

    // 将当前状态（右岸上牧师与魔鬼的数量以及船的位置）进行哈希，以记录该状态是否已经遍历
    // 该哈希函数限制了牧师与魔鬼的数量应小于10
    public int Hash(State state, bool boatOnLeft) {
        return state.preist * 10 + state.devil + (boatOnLeft ? 1 : 0) * 100;

    }

    // 返回移动的牧师和魔鬼的数量，(0, 0)表示没有找到可行路径
    public Tuple<int, int> GetNextStep(int x, int y, bool boatOnLeft) {
        // 如果本身状态不是可行状态，则返回错误
        if (!ContainState(new State(x, y))) {
            Debug.Log("wrong state");
            Debug.Log(x);
            Debug.Log(y);
            return Tuple.Create(0, 0);
        }

        // 定义记录状态遍历形况的字典
        Dictionary<int, bool> vis = new Dictionary<int, bool>();
        foreach (State state in states) {
            vis[Hash(state, false)] = false;
            vis[Hash(state, true)] = false;
        }


        // 定义队列以及初始化
        List<Node> que = new List<Node>();
        que.Add(new Node(new State(x, y), boatOnLeft, -1));
        vis[Hash(que[0].state, boatOnLeft)] = true;
        Node front = que[0];

        // 定义五个移动方向
        int[] dirx = { 1, 0, 2, 0, 1 };
        int[] diry = { 0, 1, 0, 2, 1 };
        int head = 0, tail = -1, xx, yy;

        // BFS过程
        while (head <= que.Count) {
            // 取出队列头元素
            front = que[head];
            xx = front.state.preist;
            yy = front.state.devil;
            boatOnLeft = front.boatOnLeft;

            // 判断是否已经到达终点状态
            if (xx == 0 && yy == 0) {
                break;
            }

            // 判断是否能够移动，5种方案
            for (int i = 0; i < 5; i++) {
                // 船在右岸
                if (!boatOnLeft) {
                    if (xx >= dirx[i] && yy >= diry[i]) {
                        State nextState = new State(xx - dirx[i], yy - diry[i]);
                        Node nextNode = new Node(nextState, !boatOnLeft, head);
                        int hash = Hash(nextState, !boatOnLeft);
                        if (ContainState(nextState) && !vis[hash]) {
                            que.Add(nextNode);
                            vis[hash] = true;
                        }
                    }
                } else {
                    if (count - xx >= dirx[i] && count - yy >= diry[i]) {
                        State nextState = new State(xx + dirx[i], yy + diry[i]);
                        Node nextNode = new Node(nextState, !boatOnLeft, head);
                        int hash = Hash(nextState, !boatOnLeft);
                        if (ContainState(nextState) && !vis[hash]) {
                            que.Add(nextNode);
                            vis[hash] = true;
                        }
                    }
                }
            }

            // 更新队列头和队列尾以及船的位置
            head++;
            if (head == tail) {
                tail = que.Count;
            }
        }

        // 递归寻找最开始的那一步
        while (front.parent != 0 && front.parent != -1) {
            front = que[front.parent];
        }

        // 返回下一步该移动的牧师和魔鬼的数量
        if (front.parent == 0) {
            return Tuple.Create(Math.Abs(front.state.preist - x), Math.Abs(front.state.devil - y));
        }

        // 返回错误
        return Tuple.Create(0, 0);
    }
}
