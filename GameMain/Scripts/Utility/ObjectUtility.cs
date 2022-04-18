using System;
using System.Collections.Generic;

namespace AltarOfSword
{
    public class ObjectGroup<T>
    {
        protected List<T> objs;
        protected List<T> awakeObjs;
        protected List<T> sleepObjs;
        public List<T> AwakeObjs => awakeObjs;
        public List<T> SleepObjs => sleepObjs;
        public ObjectGroup()
        {
            objs = new List<T>();
            awakeObjs = new List<T>();
            sleepObjs = new List<T>();
        }
        public virtual T Allocate()
        {
            T temp;
            if (sleepObjs.Count == 0)
            {
                temp = Activator.CreateInstance<T>();
                objs.Add(temp);
            }
            else
            {
                temp = (T)sleepObjs[0];
                sleepObjs.Remove(temp);
            }
            awakeObjs.Add(temp);
            return temp;
        }

        public virtual void Recycle(T t)
        {
            if (awakeObjs.Contains(t)) awakeObjs.Remove(t);
            if (!sleepObjs.Contains(t)) sleepObjs.Add(t);
        }

        public virtual void Clear()
        {
            this.objs.Clear();
            this.awakeObjs.Clear();
            this.sleepObjs.Clear();
        }
    }

}
