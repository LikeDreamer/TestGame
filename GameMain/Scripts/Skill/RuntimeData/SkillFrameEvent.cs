using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AltarOfSword
{
    public class SkillEvent: IReference
    {
        private int frameCount;
        private UnityAction<SkillEvent> ExitAction { get; set; }
        private UnityAction<SkillEvent> StayAction { get; set; }
        public bool IsComplete { get; private set; }
        public SkillEvent()
        {
            IsComplete = true;
            frameCount = 0;
        }
        public void AddListener(int frameCount, UnityAction<SkillEvent> exitAction, UnityAction<SkillEvent> stayAction)
        {
            this.frameCount = frameCount;
            this.ExitAction = exitAction;
            this.StayAction = stayAction;
            IsComplete = false;
        }
        public void SetFrame(int frameCount)
        {
            this.frameCount = frameCount;
            this.IsComplete = false;
        }

        public void Update()
        {
            if (IsComplete) return;
            this.frameCount--;
            StayAction?.Invoke(this);
            if (this.frameCount <= 0)
            {
                ExitAction?.Invoke(this);
                IsComplete = true;
            }
        }
        public void Continue()
        {
            SetFrame(1);
        }

        public void Interrupt()
        {
            SetFrame(0);
        }

        public void Interrupt(bool isClear)
        {
            SetFrame(0);
            if (isClear) Clear();
        }

        public void Recycle()
        {
            ReferencePool.Release(this);
        }

        public void Clear()
        {
            if (this.frameCount > 0)
            {
                ExitAction?.Invoke(this);
            }
            this.frameCount = 0;
            ExitAction = null;
            StayAction = null;
            IsComplete = true;
        }
    }

    public class SkillFrameEvent : SkillRuntimeDataPart
    {
        private List<SkillEvent> InterruptEvent { get; set; }
        private List<SkillEvent> ContinueEvent { get; set; }
        private List<SkillEvent> SkillOverEvent { get; set; }
        private List<SkillEvent> TempCollectionEvent { get; set; }
        public SkillFrameEvent(SkillRuntimeData runtimeData) : base(runtimeData)
        {
            InterruptEvent = new List<SkillEvent>();
            ContinueEvent = new List<SkillEvent>();
            SkillOverEvent = new List<SkillEvent>();
            TempCollectionEvent = new List<SkillEvent>();
        }
        public SkillEvent AddListener(int frame, UnityAction<SkillEvent> exitAction, UnityAction<SkillEvent> stayAction = null, bool isInterrupt = true)
        {
            SkillEvent skillEvent = ReferencePool.Acquire<SkillEvent>();
            skillEvent.AddListener(frame,exitAction,stayAction);
            if (isInterrupt) InterruptEvent.Add(skillEvent);
            else ContinueEvent.Add(skillEvent);
            return skillEvent;
        }

        public SkillEvent AddListener(UnityAction<SkillEvent> action)
        {
            SkillEvent skillEvent = ReferencePool.Acquire<SkillEvent>();
            skillEvent.AddListener(1,null, action);
            SkillOverEvent.Add(skillEvent);
            return skillEvent;
        }
        public void OnFrameUpdate(bool isOver)
        {
            OnUpdate(InterruptEvent);
            OnUpdate(ContinueEvent);
            OnSkillOver(isOver);
        }

        private void OnSkillOver(bool isOver)
        {
            if (!isOver) return;
            OnUpdate(SkillOverEvent);
            StopEvent(InterruptEvent);
        }

        private void OnUpdate(List<SkillEvent> collectionEvent)
        {
            if(collectionEvent.Count >0)
            {
                foreach (SkillEvent item in collectionEvent)
                {
                    item.Update();
                    if (item.IsComplete) TempCollectionEvent.Add(item);
                }
            }

            if(TempCollectionEvent.Count>0)
            {
                foreach (SkillEvent item in TempCollectionEvent)
                {
                    item.Recycle();
                    collectionEvent.Remove(item);
                }
                TempCollectionEvent.Clear();
            }
        }

        private void StopEvent(List<SkillEvent> collectionEvent)
        {
            if (collectionEvent.Count > 0)
            {
                foreach (SkillEvent item in collectionEvent)
                {
                    item.Recycle();
                }
                collectionEvent.Clear();
            }
        }
    }
}
