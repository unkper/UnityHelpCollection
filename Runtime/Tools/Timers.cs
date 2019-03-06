using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public abstract class SkillTimer
    {
        public bool finish = true;
        public abstract void Timing();
        public abstract void Update();
    }

    /// <summary>
    /// 使用前指定需要的持续时间，
    /// 如果要重置新的时间，请在那之后调用Timing
    /// 一定要使用Update来更新计时器
    /// </summary>
    public class NormalTime : SkillTimer
    {
        private float time;
        private float left = -1.0f;
        public float GetLeft { get { return left; } }
        public float SetNewTime { set { if (value >= 0.0f) time = value; } }

        public NormalTime(float time)
        {
            this.time = time;
        }

        /// <summary>
        /// 将计时器重置
        /// </summary>
        public override void Timing()
        {
            finish = false;
            left = time;
        }

        public override void Update()
        {
            if (left != -1.0f)
                left -= Time.deltaTime;
            if (left <= 0.0f && left != -1.0f)
            {
                finish = true;
                left = -1.0f;
            }
        }
    }

    public class ConfuseTime : SkillTimer
    {
        private float chance;
        private NormalTime timer;
        private NormalTime timer1;

        public ConfuseTime(float last, float judgeDelta, float chance)
        {
            timer = new NormalTime(judgeDelta);
            timer1 = new NormalTime(last);
            if (chance >= 0.0f && chance <= 1.0f)
                this.chance = chance;
            else
                Debug.LogError("无效概率");
        }

        public override void Timing()
        {
            this.finish = false;
            timer.Timing();
            timer1.Timing();
        }

        public override void Update()
        {
            timer.Update();
            timer1.Update();
            if (timer1.finish)
                finish = true;
            if (timer.finish)
            {
                float t = UnityEngine.Random.value;
                if (t <= chance)
                    finish = true;
                timer.Timing();
            }
        }
    }
}
