using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    //一定要把start和end放在绑定这个脚本的物体的外面
    public class PositionMover : MonoBehaviour
    {

        public enum MoveWays
        {
            once,
            repeat,
            pingpong,
        }
        public UnityEvent<GameObject> arrived;
        public MoveWays moveWays = MoveWays.once;
        public Vector3 start;
        public Vector3 end;
        public float speed = 1.0f;
        public bool readyToMove = false;
        public int ToEndTime = 1;   //去终点的次数
        private Vector3 target;
        private int moveTime = 0;

        void Awake()
        {
            start = transform.Find("start").position;
            end = transform.Find("end").position;
            if (start == null || end == null)
                Debug.LogError("找不到起终点");
        }

        void Start()
        {
            target = end;
            if (moveWays == MoveWays.once)
                ToEndTime = 1;
        }

        void Update()
        {
            if (readyToMove)
            {
                if (moveWays == MoveWays.pingpong)
                    PingpongMove();
                else if (moveWays == MoveWays.once)
                    Move();
                else
                    Move();
            }
        }

        void Move()
        {
            if (moveTime >= ToEndTime)
            {
                arrived.Invoke(gameObject);
                return;
            }
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
            if (transform.position == target)
            {
                if (target == end)
                {
                    moveTime++;
                    target = start;
                }
                else
                    target = end;
            }
        }

        /// <summary>
        /// 从start到end(false)，或者end到start(true)
        /// </summary>
        void PingpongMove()
        {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
            if (transform.position == target)
            {
                if (target == end)
                    target = start;
                else
                    target = end;
            }
        }
    }
}

