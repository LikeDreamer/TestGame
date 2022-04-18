using Spine.Unity;
using System.Collections;
using System.Collections.Generic;

namespace AltarOfSword
{
    public class SkillAnimation : SkillRuntimeDataPart
    {
        public SkeletonAnimation SkeletonAnimation { get; private set; }
        public float TimeScale{get=>SkeletonAnimation.timeScale;set=>SkeletonAnimation.timeScale=value;}
        public SkillAnimation(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            SkeletonAnimation = runtimeData.SkillLogic.Entity.GetComponentInChildren<SkeletonAnimation>();
        }
    }
}
