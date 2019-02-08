using System;
using System.Collections.Generic;
using UnityEngine;

//一定要把start和end放在绑定这个脚本的物体的外面
public class PositionMover : MonoBehaviour
{
    public enum MoveWays
    {
        once,
        repeat,
        pingpong,
    }
    public MoveWays moveWays = MoveWays.once;
    public Transform start;
    public Transform end;
    public float speed = 1.0f;
    public bool readyToMove = false;
    public int ToEndTime = 1;
    private Vector3 target;
    private int moveTime = 0;

    void Start()
    {
        target = end.position;
        if (moveWays == MoveWays.once)
            ToEndTime = 1;
    }

    void Update()
    {
        if(readyToMove)
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
            return;
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
        if (transform.position == target)
        {
            if (target == end.position)
            {
                moveTime++;
                target = start.position;
            }
            else
                target = end.position;
        }
    }

    void OnDrawGizmos()
    {
        if(start&&end)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(start.position, 0.05f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(end.position, 0.05f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(start.position, end.position);
        }
        
    }

    /// <summary>
    /// 从start到end(false)，或者end到start(true)
    /// </summary>
    void PingpongMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
        if(transform.position==target)
        {
            if (target == end.position)
                target = start.position;
            else
                target = end.position;
        }
    }
}
