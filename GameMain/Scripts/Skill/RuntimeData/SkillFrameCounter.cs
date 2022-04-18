using GameFramework;
using System.Collections.Generic;

namespace AltarOfSword
{
    public class FrameCounter: IReference
    {
        private int duration = 0;
        public bool IsFinish => duration <= 0;
        public bool IsContinue { get; set;}  //技能结束时 是否继续计数
        public SkillFrameItem FrameItem { get; set; }
        public void Update(int duration)
        {
            this.duration -= duration;
        }
        public void Update()
        {
            Update(1);
        }
        public void SetFrame(int duration)
        {
            this.duration = duration;
        }
        public void Continue()
        {
            Update(-1);
        }
        public void Interrupt()
        {
            duration = 0;
            this.FrameItem = null;
        }
        public void Reset(SkillFrameItem frameItem)
        {
            this.FrameItem = frameItem;
            this.duration = this.FrameItem.Duration;
        }

        public void Reset(SkillFrameItem frameItem,int duration)
        {
            this.FrameItem = frameItem;
            this.duration = duration != 0?duration:this.FrameItem.Duration;
        }

        public void Recycle()
        {
            ReferencePool.Release(this);
        }

        public void Clear()
        {
            duration = 0;
            this.FrameItem = null;
        }
    }
    public class SkillFrameCounter : SkillRuntimeDataPart
    {
        private List<FrameCounter> frameCounters;
        private List<FrameCounter> tempFrameCounters;
        public SkillFrameCounter(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            frameCounters = new List<FrameCounter>(16);
            tempFrameCounters = new List<FrameCounter>(16);
        }
        public FrameCounter Allocate()
        {
            FrameCounter frameCounter = ReferencePool.Acquire<FrameCounter>();
            frameCounters.Add(frameCounter);
            return frameCounter;
        }

        public FrameCounter Allocate(SkillFrameItem frameItem)
        {
            FrameCounter frameCounter = Allocate();
            frameCounter.Reset(frameItem);
            return frameCounter;
        }

        public FrameCounter Allocate(SkillFrameItem frameItem, int number)
        {
            FrameCounter frameCounter = Allocate(frameItem);
            if (number != 0) frameCounter.SetFrame(number);
            return frameCounter;
        }

        public FrameCounter Allocate(SkillFrameItem frameItem, int number, bool isContinue)
        {
            FrameCounter frameCounter = Allocate(frameItem, number);
            return frameCounter;
        }

        public void Recycle(FrameCounter frameCounter)
        {
            frameCounter.Recycle();
        }

        public void AddFrameCounter(FrameCounter frameCounter)
        {
            frameCounters.Add(frameCounter);
        }

        public void OnFrameUpdate(SkillFrame skillFrame)
        {
            if(skillFrame!=null)
            {
                foreach (SkillFrameItem item in skillFrame.SkillFrameItems)
                {
                    if (item.Duration < 1) continue;
                    Allocate(item);
                }
            }
            
            if(frameCounters.Count>0)
            {
                foreach (FrameCounter item in frameCounters)
                {
                    SkillExecutor.Execute(Root.SkillLogic, item);
                    item.Update();
                    if (item.IsFinish) tempFrameCounters.Add(item);
                }
            }

            if(tempFrameCounters.Count>0)
            {
                foreach (FrameCounter item in tempFrameCounters)
                {
                    Recycle(item);
                    frameCounters.Remove(item);
                }
                tempFrameCounters.Clear();
            }
        }

        public void OnSkillOver()
        {
            if (frameCounters.Count <= 0) return;
            frameCounters.ForEach(p => { if (!p.IsContinue) tempFrameCounters.Add(p);});
            foreach (FrameCounter item in tempFrameCounters)
            {
                item.Recycle();
                frameCounters.Remove(item);
            }
            tempFrameCounters.Clear();
        }
    }
}
