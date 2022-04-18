using Google.Protobuf.Collections;

namespace AltarOfSword
{
    public class SkillInstance
    {
        public int State { get; set; }
        public int Key { get; private set; } //攻击时为命令，受击时为组
        public SkillData SkillData { get; private set; }
        public SkillInstance(int key)
        {
            State = SkillDefined.SS_Null;
            Key = key;
        }
        public SkillInstance(int key,SkillData skillData) : this(key)
        {
            SkillData = skillData;
            State = SkillDefined.SS_Idle;
        }
        public SkillInstance(int key,int skillID) : this(key)
        {
            SkillData = GameEntry.Skill.GetSkillData(skillID);
            if(SkillData!=null) State = SkillDefined.SS_Idle;
        }

        public void SetSkillData(int skillID)
        {
            if (SkillData == null || SkillData.SkillID!= skillID)
            {
                SkillData skillData = GameEntry.Skill.GetSkillData(skillID);
                if (skillData != null) SkillData = skillData;
                State = SkillDefined.SS_Idle;
            }
        }
        public SkillFrame GetSkillFrame(int frameIndex)
        {
            if (SkillData == null) return null;
            if (!SkillData.SkillFrames.TryGetValue(out SkillFrame skillFrame, p => p.StartNum == frameIndex)) return null;
            return skillFrame;
        }

        public MapField<int, int> GetConfig()
        {
            if (SkillData == null) return null;
            return SkillData.SkillConfigs;
        }
    }
}
