using UnityEngine;

namespace AltarOfSword
{
    public class ValueModifier002 : ValueModifier
    {
        private CMDItem cmdItem;
        public ValueModifier002() { }
        public ValueModifier002(SkillValueModifier valueModifier) : base(valueModifier) { }

        private float maxSpeed;
        private float aTime; //正向加速度
        private float dTime; //负向加速度
        private float currentValue = 0.0f;

        public override void Activate(params object[] args)
        {
            if (args.Length < 4) return;
            int cmd= (int)args[0];
            cmdItem = RuntimeData.Input.Input.GetCMDItem(cmd);
            IsActive = cmdItem!=null;
            maxSpeed = (float)args[1];
            aTime = (float)args[2];
            dTime = (float)args[3];
            currentValue = 0.0f;
            IsActive = true;
        }
        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            float dir =System.Math.Sign(cmdItem.Value);
            float velocity = maxSpeed * dir;
            float time = cmdItem.IsTrigger ? aTime : dTime;
            Value = Mathf.SmoothDamp(inputValue, velocity, ref currentValue, time, Mathf.Infinity, deltaTime);
            Value = Value * SlowRate;
        }

        public override void Clear()
        {
            base.Clear();
            maxSpeed = 0.0f;
            aTime = 0.0f;
            dTime = 0.0f;
            currentValue = 0.0f;
        }
    }
}
