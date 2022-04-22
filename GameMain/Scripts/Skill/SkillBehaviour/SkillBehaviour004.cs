using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AltarOfSword
{
    public class SkillBehaviour004 : SkillBehaviour //水平输入改变速度
    {
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetF3 dataset)) return;
            float data0 = dataset.Data0;
            float data1 = dataset.Data1;
            float data2 = dataset.Data2;
            ValueModifier valueModifier= logic.ValueModifier.Activate(SkillDefined.SVMK_VelocityX, 2, SkillDefined.ECMD_Hrzt, data0,data1,data2);
            logic.FrameEvent.AddListener(p=> valueModifier?.Release());
        }
    }
}
