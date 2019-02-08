using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreateHelpLine : MonoBehaviour {
    public Material lineMat;
    public Transform from;
    public Transform to;

    void DrawLineInRun(Vector3 start, Vector3 end)
    {
        GL.PushMatrix(); //保存当前Matirx  
        lineMat.SetPass(0); //刷新当前材质  
        GL.LoadPixelMatrix();//设置pixelMatrix  
        GL.LoadOrtho();
        GL.Color(Color.yellow);
        GL.Begin(GL.LINES);
        GL.Vertex(start);
        GL.Vertex(end);
        GL.End();
        GL.PopMatrix();//读取之前的Matrix  
    }

    void OnPostRender()
    {
        Debug.Log("happen");
        DrawLineInRun(from.position, to.position);
    }
}
