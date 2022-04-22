using UnityEngine;
using System;

namespace AltarOfSword
{
    public class ValueModifier005 : ValueModifier
    {
        private float value;
        public ValueModifier005() { }
        public ValueModifier005(SkillValueModifier valueModifier) : base(valueModifier) { }
        public override void Activate(params object[] args)
        {
            if (args.Length < 1) return;
            value = (float)args[0];
            IsActive = true;
        }
        public override void OnUpdate(float deltaTime)
        {
            float gravityScale= manager.GetValue(SkillDefined.SVMK_GravityRate);
            value += gravityScale*Physics2D.gravity.y*deltaTime;
            Value = value* SlowRate;
        }

        public override void Clear()
        {
            base.Clear();
            value = 0.0f;
        }
    }
}
