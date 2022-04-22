using System;
using System.Collections.Generic;

namespace AltarOfSword
{
    public class SkillATKManager : SkillManager //命令触发技能管理器
    {
        public SkillATKManager(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            SkillCollectionMap = new Dictionary<int, SkillCollection>();

            SkillCollectionMap.Add(SkillDefined.ST_Grounded, new SkillCollection());
            SkillCollectionMap.Add(SkillDefined.ST_GroundedTemp, new SkillCollection());
            SkillCollectionMap.Add(SkillDefined.ST_Airborne, new SkillCollection());
            SkillCollectionMap.Add(SkillDefined.ST_AirborneTemp, new SkillCollection());

            //SkillCollection landCollection = SkillCollectionMap[SkillDefined.ST_Sky];
            //Init(landCollection.SkillDatas);

            SkillCollection landCollection = SkillCollectionMap[SkillDefined.ST_Grounded];
            //Init(landCollection.SkillDatas);

            landCollection[SkillDefined.ECMD_Nothing] = GameEntry.Skill.GetSkillData(1000);
            landCollection[SkillDefined.ECMD_MouseLD] = GameEntry.Skill.GetSkillData(1001);
            landCollection[SkillDefined.ECMD_Hrzt] = GameEntry.Skill.GetSkillData(1002);
        }
       
        public SkillData this[int key, int cmd]
        {
            get
            {
                if (!STAssert(key)) return null;
                SkillCollection landCollection = SkillCollectionMap[key];
                return landCollection[cmd];
            }
        }


        protected override bool STAssert(int skillType)
        {
            return skillType == SkillDefined.ST_Grounded ||
                    skillType == SkillDefined.ST_Airborne ||
                    skillType == SkillDefined.ST_GroundedTemp ||
                    skillType == SkillDefined.ST_AirborneTemp;
        }
      
    }
}
