using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AltarOfSword
{
    public interface IBehaviour
    {
        void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit);
    }
    public class SkillBehaviour : IBehaviour
    {
        public virtual void Execute(ActorSkillLogic logic, FrameCounter counter,DataUnit dataUnit)
        {
        }
    }
}
