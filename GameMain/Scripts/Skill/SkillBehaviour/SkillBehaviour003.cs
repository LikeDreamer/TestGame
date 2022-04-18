using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Collections;

namespace AltarOfSword
{
    public class SkillBehaviour003 : SkillBehaviour
    {
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI1Array dataset)) return;
            RepeatedField<int> types=dataset.Data;
            IEnumerator<int> enumerator=types.GetEnumerator();
            while(enumerator.MoveNext())
            {
                Execute(logic,counter,dataUnit,enumerator.Current);
            }
        }

        private void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit,int type)
        {
            switch (type)
            {
                case 1: counter.Continue(); break;
                case 2: counter.Interrupt(); break;
                case 3: logic.DataInfo.SkillState = SkillDefined.SS_Update; break;
                case 4: logic.DataInfo.SkillState = SkillDefined.SS_Over; break;
                case 7: Execute007(logic,counter,dataUnit,type);break;//根据输入换向
                case 8:logic.StateInfo.SetDirection(-logic.StateInfo.Direction); break;//强制换向
                case 999:Execute999(logic,counter,dataUnit,type);break;//合批处理999
            }

            
        }

        private void Execute007(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit,int type)
        {
            CMDItem cmdItem=logic.Input.GetCMDItem(SkillDefined.ECMD_Hrzt);
            if(cmdItem.IsTrigger)logic.StateInfo.SetDirection(cmdItem.Value);
        }

        private void Execute999(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit,int type)
        {
        }
    }
}
