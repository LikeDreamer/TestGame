using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace AltarOfSword
{
    public class SkillExecutor
    {
        public static SkillConditionExecutor Condition { get; private set; }
        public static SkillBehaviourExecutor Behaviour { get; private set; }
        public static void Init()
        {
            Condition = new SkillConditionExecutor();
            Behaviour = new SkillBehaviourExecutor();
            Condition.Init();
            Behaviour.Init();
        }

        public static bool Execute(ActorSkillLogic logic,FrameCounter counter) //返回值：执行是否结束
        {
            Behaviour.Execute(logic, counter, Condition.Execute(logic, counter));
            return true;
        }
    }


    public class SkillBehaviourExecutor
    {
        public Dictionary<int, IBehaviour> BehaviourMap { get; private set; }
        private int count = 40;
        public void Init()
        {
            BehaviourMap = new Dictionary<int, IBehaviour>(count);
            Assembly assembly = typeof(SkillBehaviour).Assembly;
            IBehaviour behaviour;
            string classPrefix = "AltarOfSword.SkillBehaviour";
            for (int i = 1; i < count; i++)
            {
                if (!BehaviourMap.ContainsKey(i))
                {
                    string eventName = $"{classPrefix}{i:000}";
                    behaviour = assembly.CreateInstance(eventName) as IBehaviour;
                    if (behaviour == null) continue;
                    BehaviourMap.Add(i, behaviour);
                }
            }
        }

        public void Execute(ActorSkillLogic logic,FrameCounter counter, bool result)
        {
            if (counter == null) return;
            SkillFrameItem frameUnit = counter.FrameItem;
            if (frameUnit == null) return;
            RepeatedField<DataUnit> dataUnits = result ? frameUnit.TrueBehaviours : frameUnit.FalseBehaviours;
            if (dataUnits.ContainsElement())
            {
                foreach (var item in dataUnits)
                {
                    //UnityEngine.Debug.LogError($"事件------{item.Type}---{SkillUnitSelectView.GetString(item.Type,false)}");
                    if (GetSkillEvent(item.Type, out IBehaviour skillEvent))
                        skillEvent.Execute(logic, counter, item);
                }
            }
        }

        private bool GetSkillEvent(int type, out IBehaviour skillEvent)
        {
            if (!BehaviourMap.TryGetValue(type, out skillEvent))
            {
                string eventName = $"AltarOfSword.SkillBehaviour{type:000}";
                Assembly assembly = typeof(SkillBehaviour).Assembly;
                skillEvent = assembly.CreateInstance(eventName) as IBehaviour;
                if (skillEvent == null) return false;
                BehaviourMap.Add(type, skillEvent);
                return true;
            }
            return true;
        }
    }

    public class SkillConditionExecutor
    {
        public Dictionary<int, ICondition> ConditionMap { get; private set; }
        private int count = 40;
        public void Init()
        {
            ConditionMap = new Dictionary<int, ICondition>(count);
            Assembly assembly = typeof(SkillCondition).Assembly;
            ICondition condition;
            string classPrefix = "AltarOfSword.SkillCondition";
            for (int i = 1; i < count; i++)
            {
                if (!ConditionMap.ContainsKey(i))
                {
                    string eventName = $"{classPrefix}{i:000}";
                    condition = assembly.CreateInstance(eventName) as ICondition;
                    if (condition == null) continue;
                    ConditionMap.Add(i, condition);
                }
            }
        }
        public bool Execute(ActorSkillLogic logic, FrameCounter counter)
        {
            if (counter == null) return false;
            SkillFrameItem frameUnit = counter.FrameItem;
            if (frameUnit == null) return false;
            if (!frameUnit.Conditions.ContainsElement()) return true;
            bool result = GetResult(logic, counter);
            return result ^ frameUnit.IsReverse;
        }

        private bool GetResult(ActorSkillLogic logic, FrameCounter counter)
        {
            RepeatedField<DataUnit> dataUnits = counter.FrameItem.Conditions;
            foreach (DataUnit item in dataUnits)
            {
                //UnityEngine.Debug.LogError($"条件------{item.Type}---{SkillUnitSelectView.GetString(item.Type, true)}");
                if (GetSkillCondition(item.Type, out ICondition skillCondition))
                    if (!skillCondition.Execute(logic, counter, item) ^ item.IsReverse) return false;
                //!skillCondition.Execute(widget, frameUnit, item) ^ item.IsReverse逻辑运算结果与!(skillCondition.Execute(widget, frameUnit, item) ^ item.IsReverse)相同
                //!A^B=!(A^B)
            }
            return true;
        }

        private bool GetSkillCondition(int type, out ICondition skillCondition)
        {
            if (!ConditionMap.TryGetValue(type, out skillCondition))
            {
                string conditionName = $"AltarOfSword.SkillCondition{type.ToString("000")}";
                Assembly assembly = typeof(SkillCondition).Assembly;
                skillCondition = assembly.CreateInstance(conditionName) as ICondition;
                if (skillCondition == null) return false;
                ConditionMap.Add(type, skillCondition);
                return true;
            }
            return true;
        }
    }

}