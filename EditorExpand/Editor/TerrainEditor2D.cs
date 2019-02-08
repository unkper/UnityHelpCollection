using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using FinalStateMachine;

public class TerrainWindow : EditorWindow
{
    [MenuItem("编辑器扩展/地形编辑器")]
    static public void OpenWindow()
    {
        var w = GetWindow<TerrainWindow>();
        w.Show();
    }
}

[CustomEditor(typeof(TerrainInspector))]
public class TerrainEditor2D : Editor {
    float BuildDelta = 0.05f;
    float nowLeftTime = 0.0f;
    LevelEdiorBase EditorBase;
    public GameObject Select;
    public GameObject hintObject;
    Vector3 point;
    public bool StartBuild = false;
    public bool IfBrushMode = false;
    bool ifFreeMode = false;
    private FSMSystem<TerrainEditor2D> system;

    void OnEnable()
    {
        List<FSMState<TerrainEditor2D>> addStates = new List<FSMState<TerrainEditor2D>>(4);
        addStates.Add(new EditIdleMode<TerrainEditor2D>(this));
        addStates.Add(new BuildMode<TerrainEditor2D>(this,true));
        addStates.Add(new BuildMode<TerrainEditor2D>(this,false));
        addStates.Add(new EditHintMode<TerrainEditor2D>(this));
        system = new FSMSystem<TerrainEditor2D>(addStates,new DefaultGlobalState<TerrainEditor2D>(this));
        if(!EditorBase)
        {
            GameObject g;
            if ((g = GameObject.Find("Level"))==null)
            {
                g = new GameObject("Level");
                Debug.LogAssertion("cannot find Level gameobject");
                EditorBase = g.AddComponent<LevelEdiorBase>();
            }
            else
            {
                EditorBase = g.GetComponent<LevelEdiorBase>();
            }
        }
    }

    void OnDisable()
    {
        if (hintObject)
            DestroyImmediate(hintObject);
    }

    void OnSceneGUI()
    {
        SceneView sceneView = SceneView.currentDrawingSceneView;
        // 当前屏幕坐标，左上角是（0，0）右下角（camera.pixelWidth，camera.pixelHeight）
        Vector2 mousePosition = Event.current.mousePosition;
        // Retina 屏幕需要拉伸值
        float mult = 1;
#if UNITY_5_4_OR_NEWER
        mult = EditorGUIUtility.pixelsPerPoint;
#endif
        // 转换成摄像机可接受的屏幕坐标，左下角是（0，0，0）右上角是（camera.pixelWidth，camera.pixelHeight，0）
        mousePosition.y = sceneView.camera.pixelHeight - mousePosition.y * mult;
        mousePosition.x *= mult;

        // 近平面往里一些，才能看得到摄像机里的位置
        Vector3 fakePoint = mousePosition;
        fakePoint.z = 20;
        point = sceneView.camera.ScreenToWorldPoint(fakePoint);
        if (hintObject)
        {
            if (!ifFreeMode)
                hintObject.transform.position = EditorBase.GridMidPoint(point);
            else
                hintObject.transform.position = point;
        }
        sceneView.Repaint();
    }

    public override bool RequiresConstantRepaint()
    {
        return true;
    }

    public void SpawnHintObject()
    {
        hintObject = Instantiate(Select, point, Quaternion.identity);
        if(hintObject.GetComponent<SpriteRenderer>())
            hintObject.GetComponent<SpriteRenderer>().material = Resources.Load<Material>("Transparent");
        var collider = hintObject.GetComponent<Collider2D>();
        if (collider)
            collider.enabled = false;
    }

    public void DesteryHint()
    {
        if (hintObject)
            DestroyImmediate(hintObject);
    }

    public bool SpawnObject()
    {
        Vector2 spawn = point;
        if (Physics2D.OverlapBox(spawn, new Vector2(EditorBase.helpLineInt / 2, EditorBase.helpLineInt / 2), 0) == null)
        {
            //在不是自由模式下，转换至合适网格
            if (!ifFreeMode)
                spawn = EditorBase.GridMidPoint(spawn);
            GameObject parent = GameObject.Find("Terrain");
            if (parent == null)
                new GameObject("Terrain");
            var g = Instantiate(Select, spawn, Quaternion.identity);
            g.transform.parent = parent.transform;
            nowLeftTime = BuildDelta;
            return true;
        }
        return false;
    }

    void DrawGUI()
    {
        int btnWidth = 200, bthHeight = 50;
        EditorGUILayout.LabelField("按住A键来放置物体");
        Select = EditorGUILayout.ObjectField(Select, typeof(GameObject), true) as GameObject;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("放置间隔");
        BuildDelta = EditorGUILayout.Slider(BuildDelta, 0.05f, 5.0f);
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if(!StartBuild)
            StartBuild = GUILayout.Button("开始放置物件", GUILayout.Width(btnWidth), GUILayout.Height(bthHeight));
        else
            StartBuild = !GUILayout.Button("停止放置物件", GUILayout.Width(btnWidth), GUILayout.Height(bthHeight));
        EditorGUILayout.BeginVertical();
        IfBrushMode = GUILayout.Toggle(IfBrushMode, "刷子模式");
        ifFreeMode = GUILayout.Toggle(ifFreeMode, "自由模式");
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    void UpdateLeftTime()
    {
        if (nowLeftTime > 0.0f)
            nowLeftTime -= Time.deltaTime;
        else
            nowLeftTime = 0f;
    }

    public override void OnInspectorGUI()
    {
        UpdateLeftTime();
        DrawGUI();
        system.Update();
        
    }

}

public class CreateCommand
{
    private List<GameObject> objects;
    
    public void AddObject(GameObject g)
    {
        objects.Add(g);
    }

    public CreateCommand()
    {
        objects = new List<GameObject>();
    }

    public void Repeal()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            Object.DestroyImmediate(objects[i]);
        }
            
    }

}

public class EditIdleMode<T> : FSMState<T> where T : TerrainEditor2D
{
    public EditIdleMode(T owner) : base(owner)
    {
        base.stateID = StateID.EditIdleMode;
        base.AddTransition(Transition.GotoHint, StateID.HintMode);
    }
    public override void Act()
    {
        return;
    }

    public override bool OnMessage(Telegram mes)
    {
        return true;
    }

    public override Transition Reason()
    {
        if (Owner.StartBuild&&Owner.Select)
            return Transition.GotoHint;
        else return Transition.NullTransition;
    }
}

public class EditHintMode<T> : FSMState<T> where T : TerrainEditor2D
{
    public EditHintMode(T owner) : base(owner)
    {
        base.stateID = StateID.HintMode;
        base.AddTransition(Transition.GotoIdle, StateID.EditIdleMode);
        base.AddTransition(Transition.GotoBuild, StateID.BuildMode);
        base.AddTransition(Transition.GotoConstantBuild, StateID.ConstantBuildMode);
    }
    public override void Act()
    {
        if(Owner.hintObject!=Owner.Select)
        {
            Owner.DesteryHint();
            Owner.SpawnHintObject();
        }
    }

    public override void DoBeforeEntering()
    {
        Owner.SpawnHintObject();
    }

    public override void DoBeforeLeaving()
    {
        Owner.DesteryHint();
    }

    public override bool OnMessage(Telegram mes)
    {
        return true;
    }

    public override Transition Reason()
    {
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.A)
        {
            if (Owner.IfBrushMode)
                return Transition.GotoConstantBuild;
            else
                return Transition.GotoBuild;
        }
        else if (!Owner.StartBuild)
            return Transition.GotoIdle;
        else
            return Transition.NullTransition;
    }
}

public class BuildMode<T> : FSMState<T> where T : TerrainEditor2D
{
    private bool OnlyOnce = true;
    private bool hasComplete = false;
    private CreateCommand command;
    public BuildMode(T owner,bool onlyOnce) : base(owner)
    {
        OnlyOnce = onlyOnce;
        if (onlyOnce)
            stateID = StateID.BuildMode;
        else
            stateID = StateID.ConstantBuildMode;
        base.AddTransition(Transition.GotoHint, StateID.HintMode);
    }

    public override void Act()
    {
        if (!OnlyOnce && Event.current.type == EventType.KeyUp)
            hasComplete = true;
        else
            hasComplete = Owner.SpawnObject();
        if (!OnlyOnce)
            Owner.SpawnObject();
    }

    public override bool OnMessage(Telegram mes)
    {
        return true;
    }

    public override Transition Reason()
    {
        if (hasComplete)
            return Transition.GotoHint;
        else return Transition.NullTransition;
    }
}

