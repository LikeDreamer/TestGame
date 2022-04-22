using System.Collections.Generic;

namespace AltarOfSword
{
    public class SkillCollection
    {
        public Dictionary<int, SkillData> SkillDatas { get; private set; }
        public SkillCollection()
        {
            SkillDatas = new Dictionary<int, SkillData>();
        }

        public SkillData this[int key]
        {
            get
            {
                SkillDatas.TryGetValue(key, out SkillData instance);
                return instance;
            }
            set
            {
                if(!SkillDatas.ContainsKey(key))
                {
                    SkillDatas.Add(key,value);
                }
                else
                {
                    SkillDatas[key] = value;
                }
                value.Init(key);
            }
        }

        public void Destroy()
        {
            SkillDatas.Clear();
            SkillDatas = null;
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
            return skillType == SkillDefined.ST_Grounded ||
                    skillType == SkillDefined.ST_Airborne ||
                    skillType == SkillDefined.ST_GroundedTemp ||
                    skillType == SkillDefined.ST_AirborneTemp||
                    skillType == SkillDefined.ST_NullCMD;
        }

        public override void Dispose()
        {
            foreach (var item in SkillCollectionMap)
            {
                item.Value.Destroy();
            }
            SkillCollectionMap.Clear();
            SkillCollectionMap = null;
        }
    }
}
