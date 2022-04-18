using Google.Protobuf.Collections;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AltarOfSword
{
    public class SkillBehaviour011 : SkillBehaviour //√¸¡Ó¥•∑¢≈–∂®
    {
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI2Array dataset)) return;

            int skillType = logic.StateInfo.GetSkillType(false);
            RepeatedField<DatasetI2> datasets = dataset.Data;
            if (datasets.IsNone()) return;
            Dictionary<int, CMDItem> cmdDic = logic.Input.CMDDic;
            SkillDataInfo skillDataInfo = logic.DataInfo;
            RepeatedField<int> cmdBuffer = logic.Input.CMDBuffer;
            int count=cmdBuffer.Count;
            CMDItem cmdItem;
            for (var i = 0; i < count; i++)
            {
                int cmd=cmdBuffer[i];
                cmdItem=cmdDic[cmd];
                if (cmdItem.IsTrigger)
                {
                    skillDataInfo.NextCMD = cmd;
                    break;
                }
            }
           
        }
    }
}
