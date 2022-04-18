using UnityEngine;
namespace AltarOfSword
{
    public class SkillStateInfo : SkillRuntimeDataPart
    {
        public int ActorState { get; set; }
        public float Direction { get; set; }
        public bool IsLeft => Direction > 0.0f;
        public bool IsRight => Direction < 0.0f;
        public bool IsLand => ActorState == SkillDefined.AS_Land;
        public bool IsSky => ActorState == SkillDefined.AS_Sky;
        private Transform transform;
        public SkillStateInfo(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            ActorState = SkillDefined.AS_Land;
            Direction = 1.0f;
            transform=runtimeData.SkillLogic.Entity.transform;
        }

        public int GetSkillType(bool isTemp)
        {
            return (ActorState, isTemp) switch
            {
                (SkillDefined.AS_Land, true) => SkillDefined.ST_LandTemp,
                (SkillDefined.AS_Land, false) => SkillDefined.ST_Land,
                (SkillDefined.AS_Sky, true) => SkillDefined.ST_SkyTemp,
                (SkillDefined.AS_Sky, false) => SkillDefined.ST_Sky,
                (_, _) => SkillDefined.ST_Land
            };
        }

        public void SetDirection(float direction)
        {
            Direction=direction;
            int sign=System.Math.Sign(direction);
            if(sign==0)return;
            Vector3 scale=transform.localScale;
            scale.x=sign;
            transform.localScale=scale;
        }

        public void SetDirection(bool isRight)
        {
            SetDirection(isRight?1.0f:-1.0f);
        }
    }
}
