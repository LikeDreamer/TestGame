using System.Collections.Generic;
using System.Linq;

namespace AltarOfSword
{
    public class SkillBehitGroup : SkillManager
    {
        public int Type { get; set; }
        public SkillBehitGroup(SkillRuntimeData runtimeData, int type) : base(runtimeData)
        {
            SkillCollectionMap = new Dictionary<int, SkillCollection>();
            this.Type = type;
        }

        public SkillData GetRandomSkillData(int group)
        {
            SkillCollection skillCollection = this[group];
            if (skillCollection == null) return null;
            List<int> skillIDs = Enumerable.ToList(skillCollection.SkillDatas.Keys);
            int skillID=UnityEngine.Random.Range(0, skillIDs.Count);
            skillID = skillIDs[skillID];
            return skillCollection[skillID];
        }

        protected override bool STAssert(int skillType)
        {
            return Type==skillType;
        }
    }
    public class SkillUHTManager : SkillRuntimeDataPart //被击技能管理器
    {
        private Dictionary<int, SkillBehitGroup> BehitSkillMap { get; set; }
        private SkillData DefaultSkillData { get; set; }
        public SkillUHTManager(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            BehitSkillMap = new Dictionary<int, SkillBehitGroup>();
            int st = SkillDefined.ST_Grounded;
            BehitSkillMap.Add(st, new SkillBehitGroup(this.Root,st));

            st = SkillDefined.ST_Airborne;
            BehitSkillMap.Add(st, new SkillBehitGroup(this.Root, st));

            DefaultSkillData = GameEntry.Skill.GetSkillData(1000);

            GameEntry.Entity.DetachEntity
        }


        public SkillData GetSkillData(int skillGroup)
        {
            int st=Root.StateInfo.IsGrounded?SkillDefined.ST_Grounded:SkillDefined.ST_Airborne;
            SkillData skillData = DefaultSkillData;
            if (BehitSkillMap.TryGetValue(st,out SkillBehitGroup group))
            {
                skillData = group.GetRandomSkillData(skillGroup);
            }
            return skillData;
        }

        public override void Dispose()
        {
            foreach (var item in BehitSkillMap)
            {
                item.Value.Dispose();
            }
        }
    }
}
