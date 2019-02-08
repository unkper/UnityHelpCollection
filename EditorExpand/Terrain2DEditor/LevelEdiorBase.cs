using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEdiorBase : MonoBehaviour {

    public float xLen = 0.5f;
    public float ZeroRadius = 0.1f;
    [Range(1,10)] public int xNum = 1;
    public float helpLineInt = 0.1f;
    const float ratio = 9.0f / 16;
    public Vector2 ZeroPoint { get { return transform.position; } }
    public float yLen { get { return ratio * xLen; } }
    public bool showGrid = false;

    public Vector2 GridMidPoint(Vector2 vector)
    {
        int xNumber = (int)((vector.x - ZeroPoint.x) / helpLineInt);
        int yNumber = (int)((vector.y - ZeroPoint.y) / helpLineInt);
        return new Vector2(ZeroPoint.x + xNumber * helpLineInt + helpLineInt / 2, ZeroPoint.y + yNumber * helpLineInt + helpLineInt / 2);
    }

    void DrawBaseScene()
    {
        float yLen = xLen * ratio;
        Vector2 p1 = new Vector2(ZeroPoint.x + xLen * xNum, ZeroPoint.y);
        Vector2 p2 = new Vector2(ZeroPoint.x, ZeroPoint.y + yLen);
        Vector2 p3 = new Vector2(ZeroPoint.x + xLen * xNum, ZeroPoint.y + yLen);
        Debug.DrawLine(ZeroPoint, p1);
        Debug.DrawLine(ZeroPoint, p2);
        Debug.DrawLine(p3, p1);
        Debug.DrawLine(p3, p2);
        Gizmos.DrawSphere(ZeroPoint, ZeroRadius);
    }

    void DrawHelpLine()
    {
        //先画竖线
        Vector2 zero = ZeroPoint;
        float nowX = zero.x;
        while(nowX + helpLineInt <= zero.x+xLen * xNum)
        {
            nowX += helpLineInt;
            Debug.DrawLine(new Vector2(nowX, zero.y), new Vector2(nowX, zero.y + yLen));
        }
        
        float nowY = zero.y;
        while (nowY + helpLineInt <= zero.y + yLen)
        {
            nowY += helpLineInt;
            Debug.DrawLine(new Vector2(zero.x, nowY), new Vector2(zero.x+xLen * xNum, nowY));
        }
    }

    void Update()
    {
        
    }

	void OnDrawGizmos()
    {
        DrawBaseScene();
        if(showGrid)
            DrawHelpLine();
    }
}
