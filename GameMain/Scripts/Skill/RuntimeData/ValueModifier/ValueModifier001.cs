namespace AltarOfSword
{
    public class ValueModifier001 : ValueModifier
    {
        private float value;
        public ValueModifier001() { }
        public ValueModifier001(SkillValueModifier valueModifier) : base(valueModifier) { }
        public override void Activate(params object[] args)
        {
            if (args.Length < 1) return;
            value = (float)args[0];
            IsActive = true;
        }
        public override void OnUpdate(float deltaTime)
        {
            Value = value*SlowRate;
        }

        public override void Clear()
        {
            base.Clear();
            value = 0.0f;
        }
    }
}
