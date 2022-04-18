using UnityEngine;
namespace AltarOfSword
{
    public class SkillCondition002 : SkillCondition //是否有命令激活
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param Data0="比较的项目"></param>
        /// <param Data1="比较方式"></param>
        /// <param Data2="比较的值"></param> 
        /// <param Data4="误差值"></param> 
        public override bool Execute(ActorSkillLogic logic, FrameCounter counter,DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI2F2 dataset)) return false;

            float current=dataset.Data0 switch
            {
                1=>logic.RuntimeData.Rigidbody.VelocityX,
                2=>logic.RuntimeData.Rigidbody.VelocityY,
                _=>0.0f
            };
            
            float value = dataset.Data2;
            float error = dataset.Data3;

            bool boolValue = dataset.Data1 switch
            {
                1 => Approximately(current, value, error),
                2 => !Approximately(current, value, error),
                3 => !Approximately(current, value, error) && Mathf.Abs(value) > value,
                4 => !Approximately(current, value, error) && Mathf.Abs(value) < value,
                _ => false
            };
            return boolValue;
        }

        public  bool Approximately(float a, float b, float error)
        {
            return Mathf.Abs(a - b) < error;
        }
    }
}
