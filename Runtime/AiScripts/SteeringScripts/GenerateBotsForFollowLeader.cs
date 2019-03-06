using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringSys
{
    public class GenerateBotsForFollowLeader : MonoBehaviour
    {
        public GameObject orcPrefab;
        public GameObject leader;
        public int botCount;
        public float minX = 22.0f;
        public float maxX = 44.0f;
        public float minZ = -11.0f;
        public float maxZ = -99.0f;
        public float Yvalue = 1.026003f;

        // Use this for initialization
        void Start()
        {
            Vector3 spawnPosition;
            GameObject orc;
            for (int i = 0; i < botCount; i++)
            {
                spawnPosition = new Vector3(transform.position.x + Random.Range(minX, maxX), transform.position.y + Yvalue, transform.position.z + Random.Range(minZ, maxZ));
                orc = Instantiate(orcPrefab, spawnPosition, Quaternion.identity);
                orc.GetComponent<SteeringForLeaderFollowing>().leader = leader;
                orc.GetComponent<SteeringForEvade>().target = leader;
                orc.GetComponent<SteeringForEvade>().enabled = false;
                orc.GetComponent<EvadeController>().leader = leader;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
