using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringSys;

public class MonsterSpawner : MonoBehaviour {
    [System.Serializable]
    public class Spawn
    {
        public GameObject spawnThing;
        public float time;
        public int point;
        public string path;
    }
    public List<Transform> spawnPoints = new List<Transform>();
    public List<Spawn> spawns = new List<Spawn>();
    public float gizmosRadius = 0.2f;
    private float timeTemp = 0.0f;
    private int nowColor = 0;
    public Color color = Color.red;
	// Use this for initialization
	void Start () {
        timeTemp = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        timeTemp += Time.deltaTime;
        List<Spawn> removed = new List<Spawn>();
        foreach (var s in spawns)
            if (timeTemp >= s.time)
                removed.Add(s);
        foreach(var s in removed)
        {
            if(spawnPoints[s.point]!=null)
            {
                var o = Instantiate(s.spawnThing, spawnPoints[s.point].position, Quaternion.identity);
                o.GetComponent<SteeringFollowPath>().whereHasPath = s.path;
                spawns.Remove(s);
            }
            else
            {
                Debug.Log("没有设置出生点");
            }
        }
	}


    void OnDrawGizmos()
    {
        Gizmos.color = color;
        int i = 0;
        foreach(var s in spawnPoints)
        {
            if(s!=null)
            {
                Gizmos.DrawSphere(s.position, gizmosRadius);
                i++;
            }
        }
    }
}
