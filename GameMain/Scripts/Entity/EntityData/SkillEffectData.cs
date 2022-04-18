using GameFramework;
using System;
using UnityEngine;

namespace AltarOfSword
{
    public class SkillEffectData : EntityData
    {
        public DatasetI2F2 Dataset { get; set; }
        public float Direction { get; set; }
        public int ParentID { get; set; }
        public Action<SkillEffectLogic> OnHide { get; set; }
        public Action<SkillEffectLogic> OnShow { get; set; }
        public bool IsBinding => (Dataset.Data1 & 1) > 0;
        public bool IsScale => (Dataset.Data1 & 2) > 0;
        public bool IsLoop => (Dataset.Data1 & 4) > 0;

        private Vector2 offset;
        public Vector2 Offset
        {
            get
            {
                offset.x = Dataset.Data2;
                offset.y = Dataset.Data3;
                return offset;
            }
        }
        public SkillEffectData() : base()
        {
            Dataset = null;
        }

        public new static SkillEffectData Create(int entityId, int typeId, Vector3 position, Quaternion quaternion, Vector3 scale)
        {
            SkillEffectData entityData = ReferencePool.Acquire<SkillEffectData>();
            entityData.Position = position;
            entityData.Rotation = quaternion;
            entityData.Scale = scale;
            entityData.ID = entityId;
            entityData.TypeID = typeId;
            return entityData;
        }

        public static SkillEffectData Create(int entityId, int typeId)
        {
            SkillEffectData entityData = ReferencePool.Acquire<SkillEffectData>();
            entityData.ID = entityId;
            entityData.TypeID = typeId;
            return entityData;
        }

        public new void Release()
        {
            ReferencePool.Release(this);
        }

        public override void Clear()
        {
            base.Clear();
            Dataset = null;
        }
    }
}
