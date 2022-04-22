using GameFramework;
using GameFramework.DataTable;
using System.Linq;
using UnityEngine;
using  UnityGameFramework.Runtime;

namespace AltarOfSword
{
    public class SkillBehaviour014 : SkillBehaviour //������Ч
    {
        /// <summary>
        /// DatasetI5B1
        /// </summary>
        /// <param Data0="��Чʵ��ID"></param>
        /// <param Data1="��Ч��Ϊ">λ1 �Ƿ�� λ2 ��ת������ λ3�Ƿ�ѭ��  </param>
        /// <param Data2="ƫ��X"></param>
        /// <param Data3="ƫ��Y"></param>
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI2F2 dataset)) return;
            Log.Info("ִ�д���");
            int entityID = GameEntry.Entity.GenerateSerialId();
            SkillEffectData data = SkillEffectData.Create(entityID, dataset.Data0);
            data.Dataset = dataset;
            data.Direction= logic.StateInfo.Direction;
            data.ParentID = logic.Entity.ID;
            data.OnShow = p =>
            {
                logic.RuntimeData.Effect.AddEffect(p);
                p.SimulationSpeed(logic.SlowFrame.Rate);
            };
            data.OnHide = p =>
            {
                logic.RuntimeData.Effect.DelEffect(p);
            };
            GameEntry.Entity.ShowSkillEffect(data);
        }
    }
}
