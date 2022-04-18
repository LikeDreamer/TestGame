using System;
using System.Collections.Generic;

namespace AltarOfSword
{
    public class SkillATKManager : SkillManager //命令触发技能管理器
    {
        public SkillATKManager(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            SkillCollectionMap = new Dictionary<int, SkillCollection>();

            SkillCollectionMap.Add(SkillDefined.ST_Land,new SkillCollection());
            SkillCollectionMap.Add(SkillDefined.ST_LandTemp, new SkillCollection());
            SkillCollectionMap.Add(SkillDefined.ST_Sky, new SkillCollection());
            SkillCollectionMap.Add(SkillDefined.ST_SkyTemp, new SkillCollection());

            SkillCollection landCollection = SkillCollectionMap[SkillDefined.ST_Sky];
            Init(landCollection.SkillInstances);

            landCollection = SkillCollectionMap[SkillDefined.ST_Land];
            Init(landCollection.SkillInstances);

            landCollection[SkillDefined.ECMD_Nothing].SetSkillData(1000);
            landCollection[SkillDefined.ECMD_MouseLD].SetSkillData(1001);
            landCollection[SkillDefined.ECMD_Hrzt].SetSkillData(1002);
        }

        private void Init(Dictionary<int, SkillInstance> instanceDic)
        {
            for (var i = SkillDefined.ECMD_Nothing; i < SkillDefined.ECMD_Max; i++)
            {
                 instanceDic.Add(i, new SkillInstance(i));
            }
        }
        public SkillInstance this[int key, int cmd]
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
            return  skillType == SkillDefined.ST_Land || 
                    skillType == SkillDefined.ST_Sky || 
                    skillType == SkillDefined.ST_LandTemp || 
                    skillType == SkillDefined.ST_SkyTemp;
        }
    }
}
