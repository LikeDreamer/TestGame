using System.Collections;
using System.Collections.Generic;
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
        public SkillFrameInfo FrameInfo => frameInfo ??= new SkillFrameInfo(this);
        public SkillAnimation Animation => animation ??= new SkillAnimation(this);
        public SkillDataInfo DataInfo => dataInfo ??= new SkillDataInfo(this);
        public SkillFrameCounter Counter => counter ??= new SkillFrameCounter(this);
        public SkillKeyInput Input => input ??= new SkillKeyInput(this);
        public SkillSlowFrame SlowFrame => slowFrame ??= new SkillSlowFrame(this);
        public SkillFrameEvent FrameEvent => frameEvent ??= new SkillFrameEvent(this);
        public SkillATKManager ATKManager => atkManager ??= new SkillATKManager(this);
        public SkillUHTManager UHTManager => uhtManager ??= new SkillUHTManager(this);
        public SkillTRGManager TRGManager => trgManager ??= new SkillTRGManager(this);
        public SkillStateInfo StateInfo => stateInfo ??= new SkillStateInfo(this);
        public SkillValueModifier ValueModifier => valueModifier ??= new SkillValueModifier(this);
        public SkillRigidbody Rigidbody => rigidbody ??= new SkillRigidbody(this);
        public SkillShieldInfo Shield => shield ??= new SkillShieldInfo(this);
        public SkillBehitBox BehitBox => behitBox ??= new SkillBehitBox(this);
        public SkillEffectList Effect => effect ??= new SkillEffectList(this);
        public SkillRuntimeData(ActorSkillLogic skillLogic)
        {
            this.SkillLogic = skillLogic;
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
    }
}