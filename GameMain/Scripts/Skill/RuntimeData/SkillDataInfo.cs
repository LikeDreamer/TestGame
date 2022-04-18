using Google.Protobuf.Collections;
using System.Collections.Generic;

namespace AltarOfSword
{
    public partial class SkillData
    {
        public int State { get; set; }
        public int Key { get; private set; } //攻击时为命令，受击时为组
        public SkillData(int key)
        {
            State = SkillDefined.SS_Idle;
            Key = key;
        }
       
        public SkillFrame GetSkillFrame(int frameIndex)
        {
            if (!SkillFrames.TryGetValue(out SkillFrame skillFrame, p => p.StartNum == frameIndex)) return null;
            return skillFrame;
        }
    }
    public class SkillDataInfo : SkillRuntimeDataPart
    {
        public SkillInstance Instance { get; private set; }
        public SkillData SkillData => Instance.SkillData;
        public int NextCMD { get; set; }
        public int SkillState { get=> Instance.State; set=> Instance.State=value; }
        public bool IsSkillOver => SkillState == SkillDefined.SS_Over;
        public SkillInstance NextInstance { get; set; }
        public SkillDataInfo(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            Instance = Root.ATKManager[SkillDefined.AS_Land,SkillDefined.ECMD_Nothing];
            NextCMD = Instance.Key;
        }
  
        public void OnFrameUpdate(int frameIndex,out SkillFrame skillFrame)
        {
            skillFrame=Instance.GetSkillFrame(frameIndex);
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
            Instance.State = SkillDefined.SS_Update;
            Root.FrameInfo.Reset();
            NextCMD = SkillDefined.ECMD_None;
        }

        private void SkillCMDSwitch() //根据NextCMD切换
        {
            int skillType = Root.StateInfo.GetSkillType(false);
            int tempSkillType = Root.StateInfo.GetSkillType(true);

            bool isFind = GetSkillInstance(tempSkillType, NextCMD, out SkillInstance skillInstance);
            if (!isFind) isFind = GetSkillInstance(skillType, NextCMD, out skillInstance);
            if (!isFind)
            {
                NextCMD = SkillDefined.ECMD_None;
                SkillNoneSwitch();
                GetSkillInstance(skillType, NextCMD, out skillInstance);
            }
            Instance = skillInstance;
            UnityGameFramework.Runtime.Log.Error($"{NextCMD}    {Instance.SkillData.SkillID}");
        }

        public bool GetSkillInstance(int skillType,int cmd,out SkillInstance skillInstance)
        {
            skillInstance = null;
            SkillCollection skillCollection = Root.ATKManager[skillType];
            if (skillCollection==null) return false;
            skillInstance = skillCollection[cmd];
            if (skillInstance == null || skillInstance.State != SkillDefined.SS_Idle)return false;
            return true;
        }

        private bool SkillNextSwitch() //固定下一个技能切换
        {
            if (NextInstance == null) return false;
            if (NextInstance.State == SkillDefined.SS_Idle)
            {
                Instance = NextInstance;
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
            MapField<int, int> config = Instance.GetConfig();
            if(false)
            {
                Instance.State = SkillDefined.SS_Cooling;
            }
            else
            {
                Instance.State = SkillDefined.SS_Idle;
            }
        }
    }
}
