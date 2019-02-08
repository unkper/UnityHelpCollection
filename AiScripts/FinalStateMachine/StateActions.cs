using System;
using System.Collections.Generic;
using UnityEngine;
using AIscripts;

namespace FinalStateMachine
{
    public class NullAct : StateAction { }
    
    public class AnimationAct:StateAction,ICloneable
    {
        public AnimationAct(Animation control, string clip)
        {
            this.control = control;
            this.clip = clip;
        }

        Animation control;
        string clip;

        public override void DoBeforeEntering()
        {
            control.Play(clip, PlayMode.StopSameLayer);
        }

        public override void DoBeforeLeaving()
        {
            control.Stop();
        }

        public object Clone()
        {
            return new AnimationAct(control, clip);
        }
    }

    public class AnimatorAct:StateAction,ICloneable
    {
        Animator animator;
        public string transName;

        public AnimatorAct(Animator animator,string name)
        {
            this.animator = animator;
            this.transName = name;
        }

        public override void DoBeforeEntering()
        {
            animator.SetBool(transName, true);
        }

        public override void DoBeforeLeaving()
        {
            animator.SetBool(transName, false);
        }

        public object Clone()
        {
            return new AnimatorAct(animator, transName);
        }
    }

    public class SteeringAct:StateAction,ICloneable
    {
        Steering steering;
        Vehicle vehicle;

        public SteeringAct(Vehicle vehicle,Steering steering)
        {
            this.steering = steering;
            this.vehicle = vehicle;
        }

        public object Clone()
        {
            return new SteeringAct(vehicle, steering);
        }

        public override void DoBeforeEntering()
        {
            steering.enabled = true;
        }

        public override void DoBeforeLeaving()
        {
            steering.enabled = false;
            vehicle.Stop();
        }
    }

    public class MaterialAct:StateAction,ICloneable
    {
        Material material;
        Renderer renderer;
        int index;
        bool ifRemove;

        public MaterialAct(Renderer renderer,Material material,int index,bool ifremove = false)
        {
            this.renderer = renderer;
            this.material = material;
            this.ifRemove = ifremove;
            this.index = index;
            if(renderer.sharedMaterials.Length - 1<index)
            {
                var r = renderer.sharedMaterials;
                renderer.sharedMaterials = new Material[index+1];
                for(int i = 0;i<r.Length;i++)
                {
                    renderer.sharedMaterials[i] = r[i];
                }
            }

        }

        public object Clone()
        {
            return new MaterialAct(renderer, material, index, ifRemove);
        }

        public override void DoBeforeEntering()
        {
            if (ifRemove)
                renderer.sharedMaterials[index] = material;
            else
                renderer.sharedMaterials[index] = null;
        }

        public override void DoBeforeLeaving()
        {
            if (ifRemove)
                renderer.sharedMaterials[index] = null;
            else
                renderer.sharedMaterials[index] = material;
        }

    }

   
}
