using System;
using System.Collections.Generic;

namespace AltarOfSword
{
    public class SkillTRGManager : SkillManager //主动触发的技能 如下落
    {
        public SkillTRGManager(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            SkillCollectionMap = new Dictionary<int, SkillCollection>();
            SkillCollectionMap.Add(SkillDefined.ST_NullCMD, new SkillCollection());
        }
        
        public SkillInstance GetSkillInstance(int skillID)
        {
            SkillCollection skillCollection = SkillCollectionMap[SkillDefined.ST_NullCMD];
            SkillInstance skillInstance=skillCollection[skillID];
            if(skillInstance==null)
            {
                skillInstance = new SkillInstance(skillID);
                skillInstance.SetSkillData(skillID);
                skillCollection.SkillInstances.Add(skillID, skillInstance);
            }
            return skillInstance;
        }

        protected override bool STAssert(int skillType)
        {
            return skillType == SkillDefined.ST_NullCMD;
        }
    }
}
