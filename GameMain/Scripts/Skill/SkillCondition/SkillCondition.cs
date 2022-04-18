using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AltarOfSword
{
    public interface ICondition
    {
        bool Execute(ActorSkillLogic logic, FrameCounter counter,DataUnit dataUnit);
    }

    public class SkillCondition : ICondition
    {
        public virtual bool Execute(ActorSkillLogic logic, FrameCounter counter,DataUnit dataUnit)
        {
            return true;
        }
    }
}
