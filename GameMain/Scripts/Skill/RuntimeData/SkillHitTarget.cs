using GameFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace AltarOfSword
{
    public class SkillHitInfo : IReference
    {
        public Entity Entity { get; private set; }
        public object HitParam { get; private set; }
        public RaycastHit2D BehitInfo { get; private set; }
        public RaycastHit2D ShieldInfo { get; private set; }

        public void SetHitInfo(Entity entity, object hitParam, RaycastHit2D behitInfo, RaycastHit2D shieldInfo)
        {
            this.Entity = entity;
            if (hitParam != null) this.HitParam = hitParam;
            if (this.BehitInfo != default) BehitInfo = behitInfo;
            if (this.ShieldInfo != default) ShieldInfo = shieldInfo;
        }

        public void Release()
        {
            ReferencePool.Release(this);
        }

        public void Clear()
        {
            Entity = null;
            BehitInfo = default;
            ShieldInfo = default;
            HitParam = null;
        }
    }
    public class SkillHitTarget : SkillRuntimeDataPart
    {
        private List<SkillHitInfo> SkillHitInfos { get; set; }
        private List<SkillHitInfo> SkillHitInfosTemp { get; set; }
        public SkillHitTarget(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            SkillHitInfos = new List<SkillHitInfo>(4);
            SkillHitInfosTemp = new List<SkillHitInfo>(4);
        }

        public void SetHitInfo(Entity entity, object hitParam, RaycastHit2D behitInfo, RaycastHit2D shieldInfo)
        {
            SkillHitInfo skillHitInfo = SkillHitInfos.First(p => p.Entity == entity);
            if (skillHitInfo == null) skillHitInfo = ReferencePool.Acquire<SkillHitInfo>();
            skillHitInfo.SetHitInfo(entity, hitParam, behitInfo, shieldInfo);
        }

        public void ClearAll()
        {
            SkillHitInfos.Clear();
        }

        public void Clear(Func<SkillHitInfo, bool> func)
        {
            if (func == null) return;
            if (SkillHitInfos.Count <= 0) return;
            foreach (SkillHitInfo item in SkillHitInfos)
            {
                if (func(item)) SkillHitInfosTemp.Add(item);
            }
            if (SkillHitInfosTemp.Count <= 0) return;
            foreach (SkillHitInfo item in SkillHitInfosTemp)
            {
                SkillHitInfos.Remove(item);
                item.Release();
            }
            SkillHitInfosTemp.Clear();
        }

        public override void Dispose()
        {
            foreach (SkillHitInfo item in SkillHitInfos)
            {
                item.Release();
            }
            SkillHitInfos.Clear();
            SkillHitInfos = null;

            foreach (SkillHitInfo item in SkillHitInfosTemp)
            {
                item.Release();
            }
            SkillHitInfosTemp.Clear();
            SkillHitInfosTemp = null;
        }
    }
}
