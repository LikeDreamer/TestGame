using GameFramework;
using System.Linq;

namespace AltarOfSword
{
    public class SkillBehaviour021 : SkillBehaviour //ѡ��֡��
    {
        /// <summary>
        /// DatasetI5B1
        /// </summary>
        /// <param Data0="������֡"></param>
        /// <param Data1="��Ч��֡"></param>
        /// <param Data2="����֡"></param>
        /// <param Data3="��֡֡��"></param>
        /// <param Data4="���ȼ�"></param>
        /// <param Data5="��֡ϵ��"></param>
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI3F1 dataset)) return;
            float timer = dataset.Data1 * SkillDefined.FrameRate;
            bool isAnimation = (dataset.Data0 & 1) > 0;
            bool isEffect = (dataset.Data0 & 2) > 0;
            bool isPhysics = (dataset.Data0 & 4) > 0;
            logic.SlowFrame.AddSlowFrame(timer,dataset.Data3,dataset.Data2, isAnimation, isEffect, isPhysics);
        }
    }
}
