using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace AltarOfSword
{
    public class SkillRuntimeData
    {
        private SkillFrameInfo frameInfo;
        private SkillAnimation animation;
        private SkillDataInfo dataInfo;
        private SkillFrameCounter counter;
        private SkillKeyInput input;
        private SkillSlowFrame slowFrame;
        private SkillStateInfo stateInfo;
        private SkillFrameEvent frameEvent;
        private SkillATKManager atkManager;
        private SkillUHTManager uhtManager;
        private SkillTRGManager trgManager;
        private SkillValueModifier valueModifier;
        private SkillRigidbody rigidbody;
        private SkillShieldInfo shield;
        private SkillBehitBox behitBox;
        private SkillEffectList effect;

        public ActorSkillLogic SkillLogic { get; private set; }
        public SkillFrameInfo FrameInfo => frameInfo ??= GetDataPart<SkillFrameInfo>();
        public SkillAnimation Animation => animation ??= GetDataPart<SkillAnimation>();
        public SkillDataInfo DataInfo => dataInfo ??= GetDataPart<SkillDataInfo>();
        public SkillFrameCounter Counter => counter ??= GetDataPart<SkillFrameCounter>();
        public SkillKeyInput Input => input ??= GetDataPart<SkillKeyInput>();
        public SkillSlowFrame SlowFrame => slowFrame ??= GetDataPart<SkillSlowFrame>();
        public SkillFrameEvent FrameEvent => frameEvent ??= GetDataPart<SkillFrameEvent>();
        public SkillATKManager ATKManager => atkManager ??= GetDataPart<SkillATKManager>();
        public SkillUHTManager UHTManager => uhtManager ??= GetDataPart<SkillUHTManager>();
        public SkillTRGManager TRGManager => trgManager ??= GetDataPart<SkillTRGManager>();
        public SkillStateInfo StateInfo => stateInfo ??= GetDataPart<SkillStateInfo>();
        public SkillValueModifier ValueModifier => valueModifier ??= GetDataPart<SkillValueModifier>();
        public SkillRigidbody Rigidbody => rigidbody ??= GetDataPart<SkillRigidbody>();
        public SkillShieldInfo Shield => shield ??= GetDataPart<SkillShieldInfo>();
        public SkillBehitBox BehitBox => behitBox ??= GetDataPart<SkillBehitBox>();
        public SkillEffectList Effect => effect ??= GetDataPart<SkillEffectList>();

        private Dictionary<Type, SkillRuntimeDataPart> DataPartMap { get; set; }
        public SkillRuntimeData(ActorSkillLogic skillLogic)
        {
            this.SkillLogic = skillLogic;
            DataPartMap = new Dictionary<Type, SkillRuntimeDataPart>();
        }
        public T GetDataPart<T>() where T : SkillRuntimeDataPart
        {
            Type type = typeof(T);
            if (DataPartMap.TryGetValue(type, out SkillRuntimeDataPart dataPart))
            {
                return dataPart as T;
            }
            else
            {
                T instance = Activator.CreateInstance(type, this) as T;
                DataPartMap.Add(type, instance);
                return instance;
            }
        }

        public void Dispose()
        {
            foreach (var item in DataPartMap)
            {
                item.Value.Dispose();
            }
            DataPartMap.Clear();
            DataPartMap = null;
        }
    }

    public abstract class SkillRuntimeDataPart
    {
        private SkillRuntimeData runtimeData;
        public SkillRuntimeData Root => runtimeData;
        public SkillRuntimeDataPart(SkillRuntimeData runtimeData)
        {
            this.runtimeData = runtimeData;
        }
        public abstract void Dispose();
    }
}