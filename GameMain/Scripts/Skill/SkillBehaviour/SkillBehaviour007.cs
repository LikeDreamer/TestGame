namespace AltarOfSword
{
    public class SkillBehaviour007 : SkillBehaviour //设置曲线
    {
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI2 dataset)) return;
            int data0 = dataset.Data0;
            int data1 = dataset.Data1;
            ValueModifier valueModifier = logic.ValueModifier.Activate(data0, 4, data1);
            logic.FrameEvent.AddListener(p => valueModifier?.Release());
        }
    }
}
