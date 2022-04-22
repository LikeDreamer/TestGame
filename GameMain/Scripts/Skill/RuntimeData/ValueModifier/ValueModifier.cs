using GameFramework;

namespace AltarOfSword
{
    public class ValueModifier:IReference
    {
        protected SkillValueModifier manager;
        protected SkillRuntimeData RuntimeData => manager.Root;
        protected float SlowRate => manager.SlowRate;
        public float Value { get; protected set; }
        public bool IsActive { get; set; }
        public int ModifierType { get; set; }
        protected float inputValue;

        public ValueModifier()
        {
        }

        public void Initialize(SkillValueModifier valueModifier)
        {
            this.manager = valueModifier;
        }
        public ValueModifier(SkillValueModifier valueModifier)
        {
            Initialize(valueModifier);
        }

        public void Release()
        {
            manager.Release(this);
        }

        public void Activate(SkillValueModifier valueModifier,int type, params object[] args)
        {
            this.manager = valueModifier;
            this.ModifierType = type;
            Activate(args);
        }

        public virtual void Activate(params object[] args) { }

        public virtual void OnUpdate(float deltaTime)
        {
            inputValue = manager.GetValue(ModifierType);
        }

        public void SetValue()
        {
            manager.SetValue(ModifierType,Value);
        }

        public virtual void Clear()
        {
            IsActive = false;
        }
    }
}
