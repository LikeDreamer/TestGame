using UnityEngine;
namespace AltarOfSword
{
    public class SkillStateInfo : SkillRuntimeDataPart
    {
        public int ActorState { get; set; }
        public float Direction { get; set; }
        public bool IsLeft => Direction > 0.0f;
        public bool IsRight => Direction < 0.0f;
        public bool IsGrounded => ActorState == SkillDefined.AS_Grounded;
        public bool IsAirborne => ActorState == SkillDefined.AS_Airborne;
        private Transform transform;
        public SkillStateInfo(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            ActorState = SkillDefined.AS_Grounded;
            Direction = 1.0f;
            transform=runtimeData.SkillLogic.Entity.transform;
        }

        public int GetSkillType(bool isTemp)
        {
            return (ActorState, isTemp) switch
            {
                (SkillDefined.AS_Grounded, true) => SkillDefined.ST_GroundedTemp,
                (SkillDefined.AS_Grounded, false) => SkillDefined.ST_Grounded,
                (SkillDefined.AS_Airborne, true) => SkillDefined.ST_AirborneTemp,
                (SkillDefined.AS_Airborne, false) => SkillDefined.ST_Airborne,
                (_, _) => SkillDefined.ST_Grounded
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

        public override void Dispose()
        {
            transform = null;
        }
    }
}
