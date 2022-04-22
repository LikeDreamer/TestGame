using UnityEngine;

namespace AltarOfSword
{
    public class SkillRigidbody : SkillRuntimeDataPart
    {
        public bool IsSlowFrame { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public CapsuleCollider2D Collider { get; private set; }
        public float GravityScale { get => Rigidbody.gravityScale; set => Rigidbody.gravityScale = value; }
        public float VelocityX { get => GetVelocity().x; set => SetVelocity(value, GetVelocity().y); }
        public float VelocityY { get => GetVelocity().y; set => SetVelocity(GetVelocity().x, value); }
        public float SiteX { get => Collider.Get(0); set => Collider.Set(0, value); }
        public float SiteY { get => Collider.Get(1); set => Collider.Set(1, value); }
        public float SizeX { get => Collider.Get(2); set => Collider.Set(2, value); }
        public float SizeY { get => Collider.Get(3); set => Collider.Set(3, value); }

        private Vector2 initialSize;
        private Vector2 initialSite;
        private Vector2 initialVelocity;
        private float initialGravityScale;
        public SkillRigidbody(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            Rigidbody = runtimeData.SkillLogic.Entity.GetComponent<Rigidbody2D>();
            Collider = Rigidbody.GetComponent<CapsuleCollider2D>();
            IsSlowFrame = false;
            initialSize = Collider.size;
            initialSite = Collider.offset;
            initialVelocity = Vector2.zero;
            initialGravityScale = Rigidbody.gravityScale;
        }

        private void SetVelocity(float xV, float yV)
        {
            if (Rigidbody == null) return;
            Vector2 velocity = Rigidbody.velocity;
            velocity.x = xV;
            velocity.y = yV;
            Rigidbody.velocity = velocity;
        }

        private Vector2 GetVelocity()
        {
            if (Rigidbody == null) return Vector2.zero;
            return Rigidbody.velocity;
        }

        public float GetValue(int type) => type switch
        {
            SkillDefined.SVMK_GravityRate => GravityScale,
            SkillDefined.SVMK_VelocityY => VelocityY,
            SkillDefined.SVMK_VelocityX => VelocityX,
            SkillDefined.SVMK_CBoxSiteX => SiteX,
            SkillDefined.SVMK_CBoxSiteY => SiteY,
            SkillDefined.SVMK_CBoxSizeX => SizeX,
            SkillDefined.SVMK_CBoxSizeY => SizeY,
            _ => 0
        };

        public float SetValue(int type, float value) => type switch
        {
            SkillDefined.SVMK_GravityRate => GravityScale = value,
            SkillDefined.SVMK_VelocityY => VelocityY = value,
            SkillDefined.SVMK_VelocityX => VelocityX = value,
            SkillDefined.SVMK_CBoxSiteX => SiteX = value,
            SkillDefined.SVMK_CBoxSiteY => SiteY = value,
            SkillDefined.SVMK_CBoxSizeX => SizeX = value,
            SkillDefined.SVMK_CBoxSizeY => SizeY = value,
            _ => 0
        };

        public float ResetValue(int type) => type switch
        {
            SkillDefined.SVMK_GravityRate => GravityScale = initialGravityScale,
            SkillDefined.SVMK_VelocityY => VelocityY = initialVelocity.y,
            SkillDefined.SVMK_VelocityX => VelocityX = initialVelocity.x,
            SkillDefined.SVMK_CBoxSiteX => SiteX = initialSite.x,
            SkillDefined.SVMK_CBoxSiteY => SiteY = initialSite.y,
            SkillDefined.SVMK_CBoxSizeX => SizeX = initialSize.x,
            SkillDefined.SVMK_CBoxSizeY => SizeY = initialSize.y,
            _ => 0
        };


        public override void Dispose()
        {
            Rigidbody = null;
            Collider = null;
        }

        public void OnUpdate(float slowTime, float deltaTime)
        {
            if (IsSlowFrame) OnUpdate(slowTime);
            else OnUpdate(deltaTime);
        }

        private void OnUpdate(float deltaTime)
        {
            // Vector2 gravity = Physics2D.gravity * Rigidbody.gravityScale;
            // Vector2 velocity = Rigidbody.velocity + gravity * deltaTime;
            // Rigidbody.velocity = velocity;
        }
    }
}
