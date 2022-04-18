using UnityEngine;

namespace AltarOfSword
{
    public class SkillBehitBox : SkillRuntimeDataPart
    {
        public BoxCollider2D Collider { get; private set; }
        public RaycastHit2D RaycastHit { get; private set; }
        public float SiteX { get => Collider.Get(0); set => Collider.Set(0, value); }
        public float SiteY { get => Collider.Get(1); set => Collider.Set(1, value); }
        public float SizeX { get => Collider.Get(2); set => Collider.Set(2, value); }
        public float SizeY { get => Collider.Get(3); set => Collider.Set(3, value); }
        public SkillBehitBox(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            Collider = Root.Rigidbody.Collider.GetComponent<BoxCollider2D>();
        }

        public float GetValue(int type) => type switch
        {
            SkillDefined.SVMK_BBoxSiteX => SiteX,
            SkillDefined.SVMK_BBoxSiteY => SiteY,
            SkillDefined.SVMK_BBoxSizeX => SizeX,
            SkillDefined.SVMK_BBoxSizeY => SizeY,
            _ => 0
        };

        public float SetValue(int type, float value) => type switch
        {
            SkillDefined.SVMK_BBoxSiteX => SiteX = value,
            SkillDefined.SVMK_BBoxSiteY => SiteY = value,
            SkillDefined.SVMK_BBoxSizeX => SizeX = value,
            SkillDefined.SVMK_BBoxSizeY => SizeY = value,
            _ => 0
        };
    }
}
