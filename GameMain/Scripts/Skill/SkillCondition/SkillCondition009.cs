namespace AltarOfSword
{
    public class SkillCondition009 : SkillCondition //判断某个命令是否激活
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param Data0="命令ID"></param>
        public override bool Execute(ActorSkillLogic logic, FrameCounter counter,DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI1 dataset)) return false;
            CMDItem cmdItem=logic.Input.GetCMDItem(dataset.Data0);
            if(cmdItem==null)return false;
            return cmdItem.IsTrigger;
        }
    }
}
