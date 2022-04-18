using System;
using System.Collections.Generic;

namespace AltarOfSword
{
    public class SkillUHTManager : SkillManager //被击技能管理器
    {
        public SkillUHTManager(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            SkillCollectionMap = new Dictionary<int, SkillCollection>();

            //key为技能组ID
        }
       
        protected override bool STAssert(int skillType)
        {
            return skillType == SkillDefined.ST_Land ||
                    skillType == SkillDefined.ST_Sky ||
                    skillType == SkillDefined.ST_LandTemp ||
                    skillType == SkillDefined.ST_SkyTemp;
        }
    }
}
