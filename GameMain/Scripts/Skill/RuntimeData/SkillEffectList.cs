using System.Collections.Generic;
using System.Linq;
using UnityGameFramework.Runtime;

namespace AltarOfSword
{
    public class SkillEffectList : SkillRuntimeDataPart
    {
        private List<Entity> entities;
        public SkillEffectList(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            entities = new List<Entity>();
        }

        public void AddEffect(int entityID)
        {
            Entity entity = GameEntry.Entity.GetEntity(entityID);
            if (entity==null) return;
            entities.Add(entity);
        }

        public void AddEffect(SkillEffectLogic logic)
        {
            if (logic == null) return;
            entities.Add(logic.Entity);
        }

        public void DelEffect(int entityID)
        {
            Entity entity = entities.FirstOrDefault(x=>x.Id== entityID);
            if (entity == null) return;
            entities.Remove(entity);
        }

        public void DelEffect(SkillEffectLogic logic)
        {
            if (logic == null) return;
            entities.Remove(logic.Entity);
        }

        public void Release()
        {
            foreach (Entity item in entities)
            {
                GameEntry.Entity.HideEntity(item);
            }
            entities.Clear();
        }

        public void SetSlowRate(float rate)
        {
            foreach (Entity entity in entities)
            {
                if(entity.Logic is SkillEffectLogic logic)
                {
                    logic.SimulationSpeed(rate);
                }
            }
        }
    }
}
