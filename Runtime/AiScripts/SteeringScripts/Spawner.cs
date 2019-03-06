using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringSys
{
    public class Spawner:MonoBehaviour
    {
        public GameObject spawnThing;
        public int maxCount = 0;
        public float intervalSpawnTime;
        public Vector3 spawnPoint;
        public float spawnRadius;
        public bool isPlanar = false;
        public bool accordBySelfPosition = true;
        private float Timer = 0.0f;
        private int hasCreatCount = 0;

        void Start()
        {
            if(accordBySelfPosition)
            {
                spawnPoint = transform.position;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(spawnPoint, spawnRadius);
        }
        void Update()
        {
            Vector3 returnPoint = new Vector3(0, 0, 0);
            Timer += Time.deltaTime;
            if(Timer>=intervalSpawnTime&&hasCreatCount<maxCount)
            {
                
                float randomX = (Random.value - 0.5f) * 2 * spawnRadius + spawnPoint.x;
                float randomZ = (Random.value - 0.5f) * 2 * spawnRadius + spawnPoint.z;
                if(isPlanar)
                {
                    returnPoint.Set(randomX, 0, randomZ);
                }
                else
                {
                    float randomY = (Random.value - 0.5f) * 2 * spawnRadius + spawnPoint.y;
                    returnPoint.Set(randomX, randomY, randomZ);
                }
                Instantiate(spawnThing, returnPoint, Quaternion.identity);
                hasCreatCount++;
            }
        }


    }
}
