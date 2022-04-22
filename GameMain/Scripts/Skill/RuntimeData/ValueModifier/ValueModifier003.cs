using UnityEngine;

namespace AltarOfSword
{
    public class ValueModifier003 : ValueModifier
    {
        private CMDItem cmdItem;
        public ValueModifier003() { }
        public ValueModifier003(SkillValueModifier valueModifier) : base(valueModifier) { }

        private float dAccelerated; //长按加速度
        private float uAccelerated; //不安加速度

        public override void Activate(params object[] args)
        {
            if (args.Length < 3) return;
            int cmd= (int)args[0];
            cmdItem = RuntimeData.Input.Input.GetCMDItem(cmd);
            IsActive = cmdItem!=null;
            dAccelerated = (float)args[1];
            uAccelerated = (float)args[2];
        }
        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            Value = cmdItem.IsTrigger ? dAccelerated : uAccelerated;
            Value = Value * SlowRate;
        }

        public override void Clear()
        {
            base.Clear();
            dAccelerated = 0.0f;
            uAccelerated = 0.0f;
        }
    }
}
