using GameFramework;
using System;
using System.Collections.Generic;

namespace AltarOfSword
{
    public class SkillValueModifier : SkillRuntimeDataPart
    {
        public Dictionary<int, ValueModifier> ModifierMap { get; private set; }
        private List<ValueModifier> TempModifiers { get; set; }
        private List<Type> VMModes { get; }
        public float SlowRate { get; set;}
        public bool IsSlowFrame { get; set; }

        public SkillValueModifier(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            TempModifiers = new List<ValueModifier>();
            ModifierMap = new Dictionary<int, ValueModifier>();
            for (int i = 0; i < SkillDefined.SVMK_Max; i++)
            {
                ModifierMap.Add(i, null);
            }

            VMModes = new List<Type>()
            {
                typeof(ValueModifier001),
                typeof(ValueModifier002),
                typeof(ValueModifier003),
                typeof(ValueModifier004),
                typeof(ValueModifier005)
            };
        }

        public void SetSlowRate(bool isSlowFrame,float slowRate)
        {
            SlowRate = slowRate;
            IsSlowFrame = isSlowFrame;
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var item in ModifierMap)
            {
                OnUpdate(item, deltaTime);
            }

            foreach (ValueModifier item in TempModifiers)
            {
                item.Release();
            }
        }

        private void OnUpdate(KeyValuePair<int, ValueModifier> keyValuePair, float deltaTime)
        {
            ValueModifier modifier = keyValuePair.Value;
            if (modifier == null) return;
            if (!modifier.IsActive)
            {
                TempModifiers.Add(modifier);
                return;
            }
            modifier.OnUpdate(deltaTime);
            modifier.SetValue();
        }


        public ValueModifier Activate(int type, int mode, params object[] args)
        {
            if (mode < 0 || mode > VMModes.Count) return null;
            Type vmMode = VMModes[mode - 1];
            ValueModifier valueModifier = ModifierMap[type];
            if (valueModifier != null)
            {
                if (valueModifier.GetType() != vmMode)
                {
                    valueModifier.Release();
                    valueModifier = ReferencePool.Acquire(vmMode) as ValueModifier;
                }
            }
            else
            {
                valueModifier = ReferencePool.Acquire(vmMode) as ValueModifier;
            }
            if (valueModifier == null) return null;
            ModifierMap[type] = valueModifier;
            valueModifier.Activate(this, type, args);
            return valueModifier;
        }

        public ValueModifier GetValueModifier(int type)
        {
            return ModifierMap[type];
        }

        public void Release(ValueModifier valueModifier)
        {
            ModifierMap[valueModifier.ModifierType] = null;
            ReferencePool.Release(valueModifier);
        }

        public float GetValue(int type)
        {
            float value = 0.0f;
            switch (type)
            {
                case SkillDefined.SVMK_GravityRate:
                case SkillDefined.SVMK_VelocityY:
                case SkillDefined.SVMK_VelocityX:
                case SkillDefined.SVMK_CBoxSiteX:
                case SkillDefined.SVMK_CBoxSiteY:
                case SkillDefined.SVMK_CBoxSizeX:
                case SkillDefined.SVMK_CBoxSizeY: value = Root.Rigidbody.GetValue(type); break;
                case SkillDefined.SVMK_BBoxSiteX:
                case SkillDefined.SVMK_BBoxSiteY:
                case SkillDefined.SVMK_BBoxSizeX:
                case SkillDefined.SVMK_BBoxSizeY: value = Root.BehitBox.GetValue(type); break;
                case SkillDefined.SVMK_SBoxSiteX:
                case SkillDefined.SVMK_SBoxSiteY:
                case SkillDefined.SVMK_SBoxSizeX:
                case SkillDefined.SVMK_SBoxSizeY: value = Root.Shield.GetValue(type); break;
            };
            return value;
        }

        public bool SetValue(int type, float value)
        {
            switch (type)
            {
                case SkillDefined.SVMK_GravityRate:
                case SkillDefined.SVMK_VelocityY:
                case SkillDefined.SVMK_VelocityX:
                case SkillDefined.SVMK_CBoxSiteX:
                case SkillDefined.SVMK_CBoxSiteY:
                case SkillDefined.SVMK_CBoxSizeX:
                case SkillDefined.SVMK_CBoxSizeY: Root.Rigidbody.SetValue(type, value); break;
                case SkillDefined.SVMK_BBoxSiteX:
                case SkillDefined.SVMK_BBoxSiteY:
                case SkillDefined.SVMK_BBoxSizeX:
                case SkillDefined.SVMK_BBoxSizeY: Root.BehitBox.SetValue(type, value); break;
                case SkillDefined.SVMK_SBoxSiteX:
                case SkillDefined.SVMK_SBoxSiteY:
                case SkillDefined.SVMK_SBoxSizeX:
                case SkillDefined.SVMK_SBoxSizeY: Root.Shield.SetValue(type, value); break;
                default: return false;
            };
            return true;
        }

        public override void Dispose()
        {
            foreach (var item in ModifierMap)
            {
                item.Value.Release();
            }
            ModifierMap.Clear();
            ModifierMap = null;
            foreach (ValueModifier item in TempModifiers)
            {
                item.Release();
            }
            TempModifiers.Clear();
            TempModifiers = null;
            VMModes.Clear();
        }
    }
}
