using GameFramework;
using GameFramework.DataTable;
using System.Linq;
using UnityEngine;
using  UnityGameFramework.Runtime;

namespace AltarOfSword
{
    public class SkillBehaviour014 : SkillBehaviour //播放特效
    {
        /// <summary>
        /// DatasetI5B1
        /// </summary>
        /// <param Data0="特效实体ID"></param>
        /// <param Data1="特效行为">位1 是否绑定 位2 旋转或缩放 位3是否循环  </param>
        /// <param Data2="偏移X"></param>
        /// <param Data3="偏移Y"></param>
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI2F2 dataset)) return;
            Log.Info("执行代码");
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
