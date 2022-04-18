using Google.Protobuf.Collections;
using System.Collections.Generic;

namespace AltarOfSword
{
    public class SkillKeyInput : SkillRuntimeDataPart
    {
        public CMDInputBase Input { get; private set; }
        public Dictionary<int, CMDItem> CMDDic => Input.CMDDic;
        public RepeatedField<int> CMDBuffer => Input.CMDBuffer;
        public SkillKeyInput(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            Input = new KeyInputCMD();
        }

        public void SetCMDBuffer(RepeatedField<int> cmdBuffer)
        {
            Input.SetCMDBuffer(cmdBuffer);
        }

        public CMDItem GetCMDItem(int cmd)
        {
            return Input.GetCMDItem(cmd);
        }

        public void OnUpdate()
        {
            Input.Update();
        }

        public void Reset(bool isReset)
        {
            //如果一次Update中执行两次逻辑帧，对于长按的命令，reset会导致值重置，
            if (isReset)Input.Reset();
        }

        public void OnUpdateOver(bool isReset)
        {
            //如果一次Update中执行两次逻辑帧，对于长按的命令，reset会导致值重置，
            if (isReset) Input.Reset();
        }
    }
}
