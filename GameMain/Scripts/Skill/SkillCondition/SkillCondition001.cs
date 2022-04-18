namespace AltarOfSword
{
    public class SkillCondition001 : SkillCondition // «∑Ò”–√¸¡Óº§ªÓ
    {
        public override bool Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            return logic.Input.Input.IsAnyTrigger;
        }
    }
}
