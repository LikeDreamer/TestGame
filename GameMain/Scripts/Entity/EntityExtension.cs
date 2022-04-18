//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;

namespace AltarOfSword
{
    public static class EntityExtension
    {
       // 关于 EntityId 的约定：
       // 0 为无效
       // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
       // 负值用于本地生成的临时实体（如特效、FakeObject等）
       private static int serialID = 0;

       public static Entity GetGameEntity(this EntityComponent entityComponent, int entityId)
       {
           UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
           if (entity == null)
           {
               return null;
           }
           return (Entity)entity;
       }

       public static void HideEntity(this EntityComponent entityComponent, GameEntity entity)
       {
           entityComponent.HideEntity(entity.Entity);
       }

        //    public static void AttachEntity(this EntityComponent entityComponent, Entity entity, int ownerId, string parentTransformPath = null, object userData = null)
        //    {
        //        entityComponent.AttachEntity(entity.Entity, ownerId, parentTransformPath, userData);
        //    }

        //    public static void ShowMyAircraft(this EntityComponent entityComponent, MyAircraftData data)
        //    {
        //        entityComponent.ShowEntity(typeof(MyAircraft), "Aircraft", Constant.AssetPriority.MyAircraftAsset, data);
        //    }

        //    public static void ShowAircraft(this EntityComponent entityComponent, AircraftData data)
        //    {
        //        entityComponent.ShowEntity(typeof(Aircraft), "Aircraft", Constant.AssetPriority.AircraftAsset, data);
        //    }

        //    public static void ShowThruster(this EntityComponent entityComponent, ThrusterData data)
        //    {
        //        entityComponent.ShowEntity(typeof(Thruster), "Thruster", Constant.AssetPriority.ThrusterAsset, data);
        //    }

        //    public static void ShowWeapon(this EntityComponent entityComponent, WeaponData data)
        //    {
        //        entityComponent.ShowEntity(typeof(Weapon), "Weapon", Constant.AssetPriority.WeaponAsset, data);
        //    }

        //    public static void ShowArmor(this EntityComponent entityComponent, ArmorData data)
        //    {
        //        entityComponent.ShowEntity(typeof(Armor), "Armor", Constant.AssetPriority.ArmorAsset, data);
        //    }

        //    public static void ShowBullet(this EntityComponent entityCompoennt, BulletData data)
        //    {
        //        entityCompoennt.ShowEntity(typeof(Bullet), "Bullet", Constant.AssetPriority.BulletAsset, data);
        //    }

        //    public static void ShowAsteroid(this EntityComponent entityCompoennt, AsteroidData data)
        //    {
        //        entityCompoennt.ShowEntity(typeof(Asteroid), "Asteroid", Constant.AssetPriority.AsteroiAsset, data);
        //    }

        //    public static void ShowEffect(this EntityComponent entityComponent, EffectData data)
        //    {
        //        entityComponent.ShowEntity(typeof(Effect), "Effect", Constant.AssetPriority.EffectAsset, data);
        //    }

        public static void ShowMonster(this EntityComponent entityComponent, EntityData data)
        {
            entityComponent.ShowEntity(typeof(SkillEffectLogic), EntityDefined.EG_Monsters, Constant.AssetPriority.MonsterAsset, data);
        }
        public static void ShowActor(this EntityComponent entityComponent, EntityData data)
        {
            entityComponent.ShowEntity(typeof(SkillEffectLogic), EntityDefined.EG_Actors, Constant.AssetPriority.ActorAsset, data);
        }
        public static void ShowSkillActor(this EntityComponent entityComponent,int typeId)
        {
            entityComponent.ShowEntity(typeof(ActorSkillLogic), EntityDefined.EG_Actors, Constant.AssetPriority.ActorAsset, typeId);
        }

        public static void ShowEntity(this EntityComponent entityComponent, int typeId)
        {
            if (typeId > 9999 && typeId < 20000)
            {
                entityComponent.ShowEntity(typeof(ActorSkillLogic), EntityDefined.EG_Actors, Constant.AssetPriority.ActorAsset, typeId);
            }
            else if (typeId > 19999 && typeId < 30000)
            {
                entityComponent.ShowEntity(typeof(EntityLogic), EntityDefined.EG_Monsters, Constant.AssetPriority.MonsterAsset, typeId);
            }
            else if (typeId > 69999 && typeId < 80000)
            {
                entityComponent.ShowEntity(typeof(SkillEffectLogic), EntityDefined.EG_SkillEffects, Constant.AssetPriority.SkillEffectAsset, typeId);
            }
        }

        public static void ShowSkillEffect(this EntityComponent entityComponent, SkillEffectData entityData)
        {
            entityComponent.ShowEntity(entityData, typeof(SkillEffectLogic), EntityDefined.EG_SkillEffects, Constant.AssetPriority.SkillEffectAsset);
        }
      
        public static void ShowEntity(this EntityComponent entityComponent,EntityData entityData, Type logicType, string entityGroup, int priority)
        {
            IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity drEntity = dtEntity.GetDataRow(entityData.TypeID);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", entityData.TypeID.ToString());
                return;
            }
            else
            {
                IDataTable<DRAssetsPath> dtAssetsPath = GameEntry.DataTable.GetDataTable<DRAssetsPath>();
                DRAssetsPath drAssetsPath = dtAssetsPath.GetDataRow(drEntity.AssetId);
                if (drAssetsPath == null)
                {
                    Log.Warning($"资源 {drEntity.AssetId} 配置不存在");
                    return;
                }
                else
                {
                    entityComponent.ShowEntity(entityData.ID, logicType, drAssetsPath.AssetPath, entityGroup, priority, entityData);
                }
            }
        }

        public static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup, int priority, int typeId)
       {
           IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
           DREntity drEntity = dtEntity.GetDataRow(typeId);
           if (drEntity == null)
           {
               Log.Warning("Can not load entity id '{0}' from data table.", typeId.ToString());
               return;
           }
           else
            {
                IDataTable<DRAssetsPath> dtAssetsPath = GameEntry.DataTable.GetDataTable<DRAssetsPath>();
                DRAssetsPath drAssetsPath = dtAssetsPath.GetDataRow(drEntity.AssetId);
                if (drAssetsPath == null)
                {
                    Log.Warning($"资源 {drEntity.AssetId} 配置不存在");
                    return;
                }
                else
                {
                    entityComponent.ShowEntity(GenerateSerialId(entityComponent), logicType, drAssetsPath.AssetPath, entityGroup, priority, null);
                }
            }
       }
       private static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup, int priority, EntityData data)
       {
           if (data == null)
           {
               Log.Warning("Data is invalid.");
               return;
           }

           IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
           DREntity drEntity = dtEntity.GetDataRow(data.TypeID);
           if (drEntity == null)
           {
               Log.Warning("Can not load entity id '{0}' from data table.", data.TypeID.ToString());
               return;
           }
           else
           {
                IDataTable<DRAssetsPath> dtAssetsPath = GameEntry.DataTable.GetDataTable<DRAssetsPath>();
                DRAssetsPath drAssetsPath = dtAssetsPath.GetDataRow(drEntity.AssetId);

                if(drAssetsPath == null)
                {
                    Log.Warning($"资源 {drEntity.AssetId} 配置不存在");
                    return;
                }
                else
                {
                    entityComponent.ShowEntity(data.ID, logicType, dtAssetsPath.FullName, entityGroup, priority, data);
                }
            }
       }

       public static int GenerateSerialId(this EntityComponent entityComponent)
       {
           return ++serialID;
       }
    }
}
