using GameFramework;
using System.Linq;

namespace AltarOfSword
{
    public class SkillBehaviour013 : SkillBehaviour //������ж�
    {
        /// <summary>
        /// DatasetI5B1
        /// </summary>
        /// <param Data0="����ID"></param>
        /// <param Data1="֡ID"></param>
        /// <param Data2="���ID"></param>
        /// <param Data3="����֡��"></param>
        /// <param Data4="��֡��ִ��"></param>
        /// <param Data6="����ʱ�Ƿ��ж�"></param>
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI5B1 dataset)) return;
            //1.����ID   2.֡ID  3.���ID  4.����֡��  5.��֡��ִ�� 6.���ܽ����Ƿ��ж�
            SkillData SkillData=GameEntry.Skill.GetSkillData(dataset.Data0);
            if (SkillData == null) return;
            SkillFrame skillFrame = SkillData.SkillFrames.FirstOrDefault(p => p.StartNum == dataset.Data1);
            if (skillFrame == null) return;
            SkillFrameItem frameItem = skillFrame.SkillFrameItems.FirstOrDefault(p => p.Number == dataset.Data2);
            if (frameItem == null) return;

            FrameCounter frameCounter = ReferencePool.Acquire<FrameCounter>();
            frameCounter.Reset(frameItem,dataset.Data3);

            if (dataset.Data4 <= 0)
            {
                SkillExecutor.Execute(logic, frameCounter);
                if (frameCounter.IsFinish) frameCounter.Release();
            }
            else
            {
                logic.FrameEvent.AddListener(dataset.Data4, ExitAction, null, dataset.Data5);

                void ExitAction(SkillEvent skillEvent)
                {
                    if (logic.DataInfo.SkillData.State!=SkillDefined.SS_Over)
                    {
                        SkillExecutor.Execute(logic, frameCounter);
                        frameCounter.Release();
                    }
                    else
                    {
                        logic.Counter.AddFrameCounter(frameCounter);
                    }
                }
            }
        }
    }
}
