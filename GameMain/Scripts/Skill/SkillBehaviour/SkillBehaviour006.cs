using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AltarOfSword
{
    public class SkillBehaviour006 : SkillBehaviour //ÉèÖÃÖµ
    {
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI1F1 dataset)) return;
            int data0 = dataset.Data0;
            float data1 = dataset.Data1;
            int type = data0 == SkillDefined.SVMK_VelocityY ? 5 : 1;
            ValueModifier valueModifier = logic.ValueModifier.Activate(data0, type, data1);
            logic.FrameEvent.AddListener(p => valueModifier?.Release());
        }
    }
}
