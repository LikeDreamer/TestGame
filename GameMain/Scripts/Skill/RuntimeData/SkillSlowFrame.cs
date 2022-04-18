using GameFramework;
using System.Collections.Generic;
namespace AltarOfSword
{
    public class SlowFrameInfo : IReference
    {
        public int Priority { get; private set; }
        public float Timer { get; private set; }
        public float Rate { get; set; }
        public bool IsAnimation { get; private set; }
        public bool IsEffect { get; private set; }
        public bool IsPhysics { get; private set; }
        public bool IsComplete { get; private set; }

        public SlowFrameInfo()
        {
            SetData(0.0f, 1.0f, 1, false, false, false);
        }

        public SlowFrameInfo(float timer, float rate, int priority, bool isAnimation = true, bool isEffect = true, bool isPhysics = true)
        {
            SetData(timer, rate, priority, isAnimation, isEffect, isPhysics);
        }
        public SlowFrameInfo(float timer, float rate, bool isAnimation = true, bool isEffect = true, bool isPhysics = true)
        {
            SetData(timer, rate, 0, isAnimation, isEffect, isPhysics);
        }

        public void Update(float deltaTime)
        {
            if (IsComplete) return;
            this.Timer -= deltaTime;
            if (this.Timer <= 0) IsComplete = true;
        }

        public void Release()
        {
            ReferencePool.Release(this);
        }

        public void Clear()
        {
            SetData(0.0f, 1.0f, 0, false, false, false);
        }
        public void SetData(float timer, float rate, int priority, bool isAnimation = true, bool isEffect = true, bool isPhysics = true)
        {
            this.Priority = priority;
            this.Timer = timer;
            this.Rate = rate;
            this.IsAnimation = isAnimation;
            this.IsEffect = isEffect;
            this.IsPhysics = isPhysics;
            this.IsComplete = false;
        }
    }
    public class SkillSlowFrame : SkillRuntimeDataPart
    {
        private LinkedList<SlowFrameInfo> slowFrameInfos;
        private SlowFrameInfo Default { get; set; }
        private SlowFrameInfo Current { get; set; }
        public SkillSlowFrame(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            slowFrameInfos = new LinkedList<SlowFrameInfo>();
            Default = ReferencePool.Acquire<SlowFrameInfo>();
            Default.SetData(0.0f, 1.0f, 1, true, true, true);
            Current = Default;
        }
        private float deltaTime = 0.0f;
        public bool IsSlowFrame { get; private set; }
        public float Rate => Current.Rate;
        public float DeltaTime => deltaTime;
        private bool IsAnimation => Current.IsAnimation;
        private bool IsEffect => Current.IsEffect;
        private bool IsPhysics => Current.IsPhysics;

        public SlowFrameInfo AddSlowFrame(float timer, float rate, int priority, bool isAnimation = true, bool isEffect = true, bool isPhysics = true)
        {
            SlowFrameInfo slowFrameInfo = ReferencePool.Acquire<SlowFrameInfo>();
            UnityGameFramework.Runtime.Log.Info($"缓帧时长 {timer}");
            slowFrameInfo.SetData(timer, rate, priority, isAnimation, isEffect, isPhysics);
            SlowFrameInfoSort(slowFrameInfo);
            return slowFrameInfo;
        }

        private void SlowFrameInfoSort(SlowFrameInfo slowFrameInfo)
        {
            LinkedListNode<SlowFrameInfo> slowFrameInfosNode = slowFrameInfos.First;
            if (slowFrameInfosNode == null)
            {
                slowFrameInfos.AddFirst(slowFrameInfo);
            }
            else
            {
                SlowFrameInfo slowFrameInfoNode = slowFrameInfosNode.Value;
                while (slowFrameInfosNode.Next != null)
                {
                    if (slowFrameInfo.Priority > slowFrameInfoNode.Priority)
                    {
                        slowFrameInfos.AddBefore(slowFrameInfosNode, slowFrameInfo);
                        return;
                    }
                    else if (slowFrameInfo.Priority == slowFrameInfoNode.Priority)
                    {
                        if (slowFrameInfo.Rate < slowFrameInfoNode.Rate)
                        {
                            slowFrameInfos.AddBefore(slowFrameInfosNode, slowFrameInfo);
                            return;
                        }
                        else if (slowFrameInfo.Rate == slowFrameInfoNode.Rate)
                        {
                            if (slowFrameInfo.Timer >= slowFrameInfoNode.Timer)
                            {
                                slowFrameInfos.AddBefore(slowFrameInfosNode, slowFrameInfo);
                                return;
                            }
                            else
                            {
                                slowFrameInfosNode = slowFrameInfosNode.Next;
                            }
                        }
                        else
                        {
                            slowFrameInfosNode = slowFrameInfosNode.Next;
                        }
                    }
                    else
                    {
                        slowFrameInfosNode = slowFrameInfosNode.Next;
                    }
                }
                slowFrameInfos.AddLast(slowFrameInfo);
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (slowFrameInfos.Count > 0)
            {
                LinkedListNode<SlowFrameInfo> node = slowFrameInfos.First;
                SlowFrameInfo nodeValue = null;
                while (node != null)
                {
                    nodeValue = node.Value;
                    nodeValue.Update(deltaTime);
                    if (nodeValue.IsComplete)
                    {
                        slowFrameInfos.Remove(node);
                        node.Value.Release();
                    }
                    node = node.Next;
                }
            }

            if (slowFrameInfos.Count > 0)
            {
                if (Current == Default || Current.IsComplete)
                {
                    Current = slowFrameInfos.First.Value;
                    SetSlowFrame(Current);
                }
            }
            else
            {
                if (Current != Default)
                {
                    Current = Default;
                    SetSlowFrame(Current);
                }
            }

            this.deltaTime = deltaTime * Current.Rate;
        }

        private void SetSlowFrame(SlowFrameInfo info)
        {
            SetSlowFrameAnimation(info);
            SetSlowFrameEffect(info);
            SetSlowFramePhysics(info);
        }

        private void SetSlowFrameAnimation(SlowFrameInfo info)
        {
            if (!info.IsAnimation) return;
            Root.Animation.TimeScale = info.Rate;
        }

        private void SetSlowFrameEffect(SlowFrameInfo info)
        {
            if (!info.IsEffect) return;
            Root.Effect.SetSlowRate(info.Rate);
        }

        private void SetSlowFramePhysics(SlowFrameInfo info)
        {

        }

       
    }
}