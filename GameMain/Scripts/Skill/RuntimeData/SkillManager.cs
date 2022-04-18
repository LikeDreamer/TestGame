using System.Collections.Generic;

namespace AltarOfSword
{
    public class SkillCollection
    {
        public Dictionary<int, SkillInstance> SkillInstances { get; private set; }
        public SkillCollection()
        {
            SkillInstances = new Dictionary<int, SkillInstance>();
        }

        public SkillInstance this[int key]
        {
            get
            {
                SkillInstances.TryGetValue(key, out SkillInstance instance);
                return instance;
            }
        }
    }
    public class SkillManager : SkillRuntimeDataPart
    {
        protected Dictionary<int, SkillCollection> SkillCollectionMap { get; set; }
        public SkillManager(SkillRuntimeData runtimeData) : base(runtimeData)
        {
        }
    
        public SkillCollection this[int key]
        {
            get
            {
                if (!STAssert(key)) return null;
                return SkillCollectionMap[key];
            }
        }
        protected virtual bool STAssert(int skillType)
        {
            return skillType == SkillDefined.ST_Land ||
                    skillType == SkillDefined.ST_Sky ||
                    skillType == SkillDefined.ST_LandTemp ||
                    skillType == SkillDefined.ST_SkyTemp||
                    skillType == SkillDefined.ST_NullCMD;
        }
    }
}
