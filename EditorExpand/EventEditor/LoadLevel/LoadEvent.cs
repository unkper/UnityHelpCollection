using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EventEditor
{
    public class LoadEvent : m_Event {
        public enum LoadWays
        {
            Reset,
            NextLevel,
            GiveLevel
        }
        public int GiveLevelIndex = 0;
        public LoadWays loadWays = LoadWays.Reset;
        private GameObject player;
        public Transform StartPoint;

        void Start()
        {
            if ((loadWays==LoadWays.Reset)&&((player = GameObject.FindGameObjectWithTag("Player")) == null))
                Debug.Log("找不到tag为player的gameobject");
        }

        public override void PlayEvents(m_Trigger sender,object info)
        {
            switch(loadWays)
            {
                case LoadWays.Reset:
                    {
                        ResetPlayer();
                        break;
                    }
                case LoadWays.NextLevel:
                    {
                        LoadNextLevel();
                        break;
                    }
                case LoadWays.GiveLevel:
                    {
                        LoadLevel(GiveLevelIndex);
                        break;
                    }
                default:
                    {
                        Debug.Log("error");
                        break;
                    }
            }
        }

        void OnDrawGizmos()
        {
            if(loadWays==LoadWays.Reset)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(StartPoint.position, new Vector3(0.05f, 0.05f, 0.05f));
            }
        }

        void ResetPlayer()
        {
            player.transform.position = StartPoint.position;
        }

        void LoadNextLevel()
        {
            LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        void LoadLevel(int index)
        {
            if(index>=0&&index<SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(index);
            else Debug.LogError("无效的关卡加载");
        }

    }

}
