using GameFramework;
using System.Linq;

namespace AltarOfSword
{
    public class SkillBehaviour013 : SkillBehaviour //命令触发判定
    {
        /// <summary>
        /// DatasetI5B1
        /// </summary>
        /// <param Data0="技能ID"></param>
        /// <param Data1="帧ID"></param>
        /// <param Data2="编号ID"></param>
        /// <param Data3="持续帧数"></param>
        /// <param Data4="几帧后执行"></param>
        /// <param Data6="结束时是否中断"></param>
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI5B1 dataset)) return;
            //1.技能ID   2.帧ID  3.编号ID  4.持续帧数  5.几帧后执行 6.技能结束是否中断
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
