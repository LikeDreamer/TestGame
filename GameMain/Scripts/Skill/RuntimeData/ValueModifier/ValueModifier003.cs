using UnityEngine;
using System;

namespace AltarOfSword
{
    public class ValueModifier003 : ValueModifier
    {
        private AnimationCurve curve;
        private float totalTime;
        private float timer;
        public ValueModifier003() { }
        public ValueModifier003(SkillValueModifier valueModifier) : base(valueModifier) { }
        public override void Activate(params object[] args)
        {
            if (args.Length < 1) return;
            int index = (int)args[0];
            curve = GameEntry.Skill.Curves[index];
            if (curve == null || curve.keys == null || curve.keys.Length <= 0) return;
            IsActive= true;
            Array.ForEach(curve.keys, t=>totalTime=Mathf.Max(t.time,totalTime));
            timer = 0.0f;
        }
        public override void OnUpdate(float deltaTime)
        {
            if(timer> totalTime)
            {
                IsActive = false;
                return;
            }
            base.OnUpdate(deltaTime);
            timer += deltaTime;
            Value=curve.Evaluate(totalTime);
        }

        public override void Clear()
        {
            base.Clear();
            curve = null;
            totalTime = 0.0f;
            timer = 0.0f;
        }
    }
}
