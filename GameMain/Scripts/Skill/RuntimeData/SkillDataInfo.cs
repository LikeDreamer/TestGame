using Google.Protobuf.Collections;
using System.Collections.Generic;

namespace AltarOfSword
{
    public partial class SkillData
    {
        public int State { get; set; }
        public int Key { get; private set; } //攻击时为命令，受击时为组
        public float CoolingTime { get; private set;}
        public void Init(int key)
        {
            State = SkillDefined.SS_Idle;
            Key = key;
        }
        public SkillFrame GetSkillFrame(int frameIndex)
        {
            if (!SkillFrames.TryGetValue(out SkillFrame skillFrame, p => p.StartNum == frameIndex)) return null;
            return skillFrame;
        }

        public void EnterCooling()
        {
            return;
            int coolingTime=SkillConfigs[1];
            CoolingTime = UnityEngine.Time.time + coolingTime / 10000.0f;
        }
    }
    public class SkillDataInfo : SkillRuntimeDataPart
    {
        public SkillData SkillData { get; set; }
        public int NextCMD { get; set; }
        public int SkillState { get=> SkillData.State; set=> SkillData.State=value; }
        public bool IsSkillOver => SkillState == SkillDefined.SS_Over;
        public SkillData NextSkillData { get; set; }
        public SkillDataInfo(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            SkillData = Root.ATKManager[SkillDefined.AS_Grounded,SkillDefined.ECMD_Nothing];
            NextCMD = SkillData.Key;
        }
  
        public void OnFrameUpdate(int frameIndex,out SkillFrame skillFrame)
        {
            skillFrame= SkillData.GetSkillFrame(frameIndex);
        }

        public void OnSkillOver()
        {
            EnterCooling();
            if (SkillNextSwitch()) return;
            SkillCMDSwitch();
            SkillSwitchOver();
        }

        private void SkillSwitchOver() //切换技能结束
        {
            SkillData.State = SkillDefined.SS_Update;
            Root.FrameInfo.Reset();
            NextCMD = SkillDefined.ECMD_None;
        }

        private void SkillCMDSwitch() //根据NextCMD切换
        {
            int skillType = Root.StateInfo.GetSkillType(false);
            int tempSkillType = Root.StateInfo.GetSkillType(true);

            bool isFind = GetSkillData(tempSkillType, NextCMD, out SkillData skillData);;
            if (!isFind) isFind = GetSkillData(skillType, NextCMD, out skillData);
            if (!isFind)
            {
                NextCMD = SkillDefined.ECMD_None;
                SkillNoneSwitch();
                GetSkillData(skillType, NextCMD, out skillData);
            }
            SkillData = skillData;
            UnityGameFramework.Runtime.Log.Error($"{NextCMD}    {skillData.SkillID}");
        }

        public bool GetSkillData(int skillType,int cmd,out SkillData skillData)
        {
            skillData = null;
            SkillCollection skillCollection = Root.ATKManager[skillType];
            if (skillCollection==null) return false;
            skillData = skillCollection[cmd];
            if (skillData == null || skillData.State != SkillDefined.SS_Idle)return false;
            return true;
        }

        private bool SkillNextSwitch() //固定下一个技能切换
        {
            if (NextSkillData == null) return false;
            if (NextSkillData.State == SkillDefined.SS_Idle)
            {
                SkillData = NextSkillData;
                return true;
            }
            return false;
        }

        private void SkillNoneSwitch() //无后续技能切换
        {
            if (NextCMD != SkillDefined.ECMD_None) return;
            //没有后续技能则切换到
            NextCMD = SkillDefined.ECMD_Nothing;
        }

        private void EnterCooling() //进入冷却或者进入空闲
        {
            MapField<int, int> config = SkillData.SkillConfigs;
            if(false)
            {
                SkillData.State = SkillDefined.SS_Cooling;
            }
            else
            {
                SkillData.State = SkillDefined.SS_Idle;
            }
        }

        public override void Dispose()
        {
            SkillData = null;
            NextCMD = 0;
        }
    }
}
