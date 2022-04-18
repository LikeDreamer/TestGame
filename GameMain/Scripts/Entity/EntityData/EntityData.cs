using System;
using UnityEngine;
using GameFramework;

namespace AltarOfSword
{
    public class EntityData : IReference
    {
        public int ID{ get; protected set; }
        public int TypeID { get; protected set; }
        public Vector3 Position { get; protected set; }
        public Quaternion Rotation { get; protected set; }
        public Vector3 Scale { get; protected set; }

        public EntityData()
        {
            Clear();
        }

        public EntityData(int entityId, int typeId)
        {
            ID = entityId;
            TypeID = typeId;
        }

        public static EntityData Create(Vector3 position, Quaternion quaternion, Vector3 scale)
        {
            EntityData entityData = ReferencePool.Acquire<EntityData>();
            entityData.Position = position;
            entityData.Rotation = quaternion;
            entityData.Scale = scale;
            return entityData;
        }

        public static EntityData Create(int entityId,int typeId, Vector3 position, Quaternion quaternion, Vector3 scale)
        {
            EntityData entityData = Create(position,quaternion,scale);
            entityData.ID = entityId;
            entityData.TypeID = typeId;
            return entityData;
        }

        public void Release()
        {
            ReferencePool.Release(this);
        }
        public virtual void Clear()
        {
            Position = Vector3.zero;
            Rotation = Quaternion.identity;
            Scale = Vector3.one;
        }
    }
}
