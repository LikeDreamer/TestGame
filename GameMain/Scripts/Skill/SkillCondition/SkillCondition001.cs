namespace AltarOfSword
{
    public class SkillCondition001 : SkillCondition //�Ƿ��������
    {
        public override bool Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            return logic.Input.Input.IsAnyTrigger;
        }
    }
}
