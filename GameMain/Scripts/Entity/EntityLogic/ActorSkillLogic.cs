using UnityGameFramework.Runtime;

namespace AltarOfSword
{
    public class ActorSkillLogic : EntityLogic
    {
        public SkillRuntimeData RuntimeData { get; private set; }
        public SkillFrameInfo FrameInfo => RuntimeData.FrameInfo;
        public SkillDataInfo DataInfo => RuntimeData.DataInfo;
        public SkillFrameCounter Counter => RuntimeData.Counter;
        public SkillKeyInput Input => RuntimeData.Input;
        public SkillSlowFrame SlowFrame=> RuntimeData.SlowFrame;
        public SkillFrameEvent FrameEvent => RuntimeData.FrameEvent;
        public SkillStateInfo StateInfo => RuntimeData.StateInfo;
        public SkillRigidbody Rigidbody => RuntimeData.Rigidbody;
        public SkillValueModifier ValueModifier => RuntimeData.ValueModifier;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            RuntimeData = new SkillRuntimeData(this);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            //Log.Info($"{elapseSeconds}  {realElapseSeconds}");
            Input.OnUpdate();

            OnFrameUpdate(elapseSeconds,realElapseSeconds);

            SlowFrame.OnUpdate(realElapseSeconds);

            ValueModifier.OnUpdate(realElapseSeconds);

            //Counter.OnUpdate();
            //Rigidbody.OnUpdate(SlowFrame.DeltaTime,realElapseSeconds);

            Input.OnUpdateOver(FrameInfo.IsChange);

            FrameInfo.OnUpdate(SlowFrame.DeltaTime);
        }


        private void OnFrameUpdate(float elapseSeconds, float realElapseSeconds)
        {
            for (int i = FrameInfo.Previous; i < FrameInfo.Current; i++)
            {
                DataInfo.OnFrameUpdate(i, out SkillFrame skillFrame);

                Counter.OnFrameUpdate(skillFrame);
                //Log.Error($"¼¼ÄÜË¢ÐÂ_____i:{i.ToString()}-----count:{FrameInfo.Count}----previous:{FrameInfo.Previous}-----current:{FrameInfo.Current}");

                FrameInfo.OnFrameUpdateOver(1);

                FrameEvent.OnFrameUpdate(DataInfo.IsSkillOver);

                if (DataInfo.IsSkillOver)
                {
                    Counter.OnSkillOver();
                    DataInfo.OnSkillOver();
                }
            }
        }
        protected  override void OnRecycle()
        {
            base.OnRecycle();
            RuntimeData.Dispose();
        }
    }
}

