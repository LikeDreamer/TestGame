using UnityEngine;

namespace AltarOfSword
{
    public class SkillShieldInfo : SkillRuntimeDataPart
    {
        public BoxCollider2D Collider { get; private set; }
        public RaycastHit2D RaycastHit { get; private set; }
        public float SiteX { get => Collider.Get(0); set => Collider.Set(0, value); }
        public float SiteY { get => Collider.Get(1); set => Collider.Set(1, value); }
        public float SizeX { get => Collider.Get(2); set => Collider.Set(2, value); }
        public float SizeY { get => Collider.Get(3); set => Collider.Set(3, value); }
        public SkillShieldInfo(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            Collider = Root.Rigidbody.Collider.GetComponent<BoxCollider2D>();
        }

        public void Activate(Vector3 position, Vector3 size)
        {
            if (size.x<0.1f||size.y<0.1f)
            {
                ResetShield();
                return;
            }
            if (Collider == null)
            {
                GameObject gameObject = Root.BehitBox.Collider.gameObject;
                Collider = gameObject.AddComponent<BoxCollider2D>();
                Collider.isTrigger = true;
            }
            Collider.enabled = true;
            Collider.offset = position;
            Collider.size = size;
        }

        public void ResetShield()
        {
            if (Collider != null) Collider.enabled = false;
        }

        public float GetValue(int type) => type switch
        {
            SkillDefined.SVMK_SBoxSiteX => SiteX,
            SkillDefined.SVMK_SBoxSiteY => SiteY,
            SkillDefined.SVMK_SBoxSizeX => SizeX,
            SkillDefined.SVMK_SBoxSizeY => SizeY,
            _ => 0
        };

        public float SetValue(int type,float value) => type switch
        {
            SkillDefined.SVMK_SBoxSiteX => SiteX=value,
            SkillDefined.SVMK_SBoxSiteY => SiteY=value,
            SkillDefined.SVMK_SBoxSizeX => SizeX=value,
            SkillDefined.SVMK_SBoxSizeY => SizeY=value,
            _ => 0
        };
    }
}
