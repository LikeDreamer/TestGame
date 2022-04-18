using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AltarOfSword
{
    public class SkillBehaviour002 : SkillBehaviour //走跑跳落切换
    {
        /// <summary>
        /// DatasetI4
        /// </summary>
        /// <param Data0="停"></param>
        /// <param Data1="走"></param>
        /// <param Data2="跳"></param>
        /// <param Data3="落"></param>
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI4 dataset)) return;
            SkillDataInfo dataInfo = logic.DataInfo;
            if (dataInfo.NextCMD == (int)SkillDefined.ECMD_None)
            {
                SkillStateInfo stateInfo = logic.StateInfo;
                int skillID = 0;
                if (stateInfo.IsSky)
                {
                    dataInfo.NextInstance = logic.RuntimeData.TRGManager.GetSkillInstance(dataset.Data3); //设置下个技能为下落
                }
                else
                {
                    CMDInputBase input = logic.Input.Input;
                    CMDItem inputCMD = input.GetCMDItem(SkillDefined.ECMD_Jump);
                    if (inputCMD.IsTrigger)
                    {
                        dataInfo.NextCMD = (int)SkillDefined.ECMD_JumpD; //设置下个技能为跳
                    }
                    else
                    {
                        inputCMD = input.GetCMDItem(SkillDefined.ECMD_Hrzt);
                        if (inputCMD.IsTrigger)
                        {
                            dataInfo.NextCMD = (int)SkillDefined.ECMD_Hrzt;//设置下个技能为走
                        }
                    }
                }
                if (skillID==0)
                {
                    dataInfo.NextCMD = (int)SkillDefined.ECMD_Nothing;//设置下个技能为待机
                }
            }
        }
    }
}
