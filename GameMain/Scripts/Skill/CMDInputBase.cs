using Google.Protobuf.Collections;
using System.Collections.Generic;

namespace AltarOfSword
{
    public class CMDInputBase
    {
        public virtual List<int> IgnoreCMDs { get;}
        public Dictionary<int, CMDItem> CMDDic { get; private set; } = new Dictionary<int, CMDItem>();
        public RepeatedField<int> CMDBuffer { get; set; }

        private bool isAnyTrigger = false;
        public bool IsAnyTrigger
        {
            protected set => isAnyTrigger = isAnyTrigger || value;
            get => isAnyTrigger;
        }
 
        public void SetCMDBuffer(RepeatedField<int> cmdBuffer)
        {
            if (cmdBuffer == null || cmdBuffer.Count <= 0) return;
            this.CMDBuffer = cmdBuffer;
            this.isAnyTrigger = false;
            foreach (int item in cmdBuffer) //保证设置缓冲区后，后续判断是正常的！
            {
                isAnyTrigger |= CMDDic[item].IsTrigger;
            }
        }
       
        public CMDItem GetCMDItem(int cmd)
        {
            CMDDic.TryGetValue(cmd, out CMDItem cmdItem);
            return cmdItem;
        }
        protected bool SetCMDState(int cmd, bool isTrigger)
        {
            return SetCMDState(cmd, isTrigger, isTrigger ? 1.0f : 0.0f);
        }

        public bool SetCMDState(int cmd, bool isTrigger, float value)
        {
            CMDItem cmdItem = CMDDic[cmd];
            if (!cmdItem.IsTrigger) cmdItem.SetState(isTrigger,value);
          
            //return !NotCMDs.Contains(cmd)&&(CMDBuffer.Contains(cmd))&&cmdItem.IsTrigger;
            //NotCMDs 和 CMDBuffer不重叠
            return (CMDBuffer.Contains(cmd)) && cmdItem.IsTrigger;
        }
        public void Reset()
        {
            isAnyTrigger = false;
            foreach (var item in CMDDic)
            {
                item.Value.Reset();
            }
        }
        public virtual void Update()
        {
        }
    }

    public class CMDItem
    {
        private int cmd;
        private bool isTrigger;
        private float value;
        public int CMD { get => cmd; set => cmd = value; }
        public bool IsTrigger { get => isTrigger; set => isTrigger = value; }
        public float Value { get => value; set => this.value = value; }
        public CMDItem(int cmd)
        {
            this.cmd = cmd;
            this.isTrigger = false;
            this.value = 0.0f;
        }

        public void SetState(bool isTrigger, float value)
        {
            this.isTrigger = isTrigger;
            this.value = value;
        }
        public void Reset()
        {
            SetState(false,0.0f);
        }
    }
}

