using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace EventEditor
{
    public class TextChangeEvent : m_Event
    {
        public Text text;
        public changeText[] texts = new changeText[3];
        public bool ifByClick = true;

        [Serializable]
        public class changeText
        {
            public string content;
            public float time;
        }

        public override void PlayEvents(m_Trigger sender, object info)
        {
            if (ifByClick)
                StartCoroutine(changeByClick());
            else
                StartCoroutine(changeByTime());
        }

        IEnumerator changeByClick()
        {
            int i = 0;
            while(texts.Length>0)
            {
                yield return null;
            }
        }

        IEnumerator changeByTime()
        {
            while(texts.Length>0)
            {
                yield return null;
                List<changeText> remove = new List<changeText>();
                foreach (var o in texts)
                {
                    if (o.time <= 0.0f)
                        remove.Add(o);
                    else
                        o.time -= Time.deltaTime;
                }
                foreach (var o in remove)
                {
                    text.text = o.content;
                }
            }

        }
    }
}
