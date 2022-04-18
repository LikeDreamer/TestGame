namespace AltarOfSword
{
    public class SkillBehaviour012 : SkillBehaviour //设置命令列表
    {
        public override void Execute(ActorSkillLogic logic, FrameCounter counter,DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI1Array dataset)) return;
            logic.Input.SetCMDBuffer(dataset.Data);
        }
    }
}
