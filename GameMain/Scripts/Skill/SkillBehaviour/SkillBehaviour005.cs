namespace AltarOfSword
{
    public class SkillBehaviour005 : SkillBehaviour //垂直输入改变加速度
    {
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetF2 dataset)) return;
            float data0 = dataset.Data0;
            float data1 = dataset.Data1;
            ValueModifier valueModifier= logic.ValueModifier.Activate(SkillDefined.SVMK_GravityRate, 3, SkillDefined.ECMD_Vert, data0,data1);
            logic.FrameEvent.AddListener(p=> valueModifier?.Release());
        }
    }
}
