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
        
        public SkillData GetSkillData(int skillID)
        {
            SkillCollection skillCollection = SkillCollectionMap[SkillDefined.ST_NullCMD];
            SkillData skillData =skillCollection[skillID];
            if(skillData == null)
            {
                skillData = GameEntry.Skill.GetSkillData(skillID);
                skillCollection.SkillDatas.Add(skillID, skillData);
            }
            return skillData;
        }

        protected override bool STAssert(int skillType)
        {
            return skillType == SkillDefined.ST_NullCMD;
        }
    }
}
