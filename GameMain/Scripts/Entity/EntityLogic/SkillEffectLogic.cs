using UnityGameFramework.Runtime;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace AltarOfSword
{

    public class SkillEffectItemLogic : MonoBehaviour
    {
        private ParticleSystem particle;
        public ParticleSystem Particle => particle ??= this.GetComponent<ParticleSystem>();
        private float lifetime;
        private float simulationSpeed;
        public float Lifetime => lifetime;
        public void Init()
        {
            lifetime = EffectExtension.GetDuration(Particle);
            ParticleSystem.MainModule main = Particle.main;
            simulationSpeed = main.simulationSpeed;
        }

        public void SimulationSpeed(float rate)
        {
            ParticleSystem.MainModule main = Particle.main;
            main.simulationSpeed = simulationSpeed * rate;
        }

        public void Simulate(float t)
        {
            particle.Simulate(t);
        }

        public void Play()
        {
            particle.Play();
        }
    }
    public class SkillEffectLogic : EntityLogic
    {
        private SkillEffectData Data { get; set;}
        private float duration;
        private List<SkillEffectItemLogic> particlesItem;
        private float timer;
        private float rate;
        public float Duration => duration;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            particlesItem = new List<SkillEffectItemLogic>();
            ParticleSystem[] particles = this.GetComponentsInChildren<ParticleSystem>();
            foreach (var item in particles)
            {
                particlesItem.Add(item.gameObject.AddComponent<SkillEffectItemLogic>());
            }
            particlesItem.ForEach(p => p.Init());
            if(particlesItem.Count>0) duration = particlesItem.Max(p => p.Lifetime);
            if (duration <= 0) duration = 1.0f;
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            if (userData is SkillEffectData data)
            {
                Data = data;
                Entity entity = GameEntry.Entity.GetEntity(Data.ParentID);
                if (Data.IsBinding)
                {
                    GameEntry.Entity.AttachEntity(this.Entity, entity);
                }

                Vector3 position = Data.Offset;
                position.x = position.x * Data.Direction;
                position += entity.transform.position;
                this.transform.position=position;
                if (Data.IsScale)
                {
                    Vector3 scale = this.transform.localScale;
                    scale.x =Mathf.Abs(scale.x) * Data.Direction;
                    this.transform.localScale = scale;
                }
                else
                {
                    float angle = Data.Direction > 0.0f ? 0.0f : 180.0f;
                    Quaternion rotation = Quaternion.Euler(0.0f, angle, 0.0f);
                    this.transform.localRotation = rotation;
                }
                Data.OnShow(this);
            }
            Simulate(0.0f);
            Play();
            timer = duration * 1.1f;
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (Data.IsLoop) return;
            if(timer<=0)
            {
                GameEntry.Entity.HideEntity(this.Entity);
                return;
            }
            timer -= realElapseSeconds * rate;
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown,userData);
            if(Data!=null) 
            {
                Data.OnHide?.Invoke(this);
                Data.Release();
            }
        }

        public void SimulationSpeed(float rate)
        {
            this.rate = rate;
            foreach (var item in particlesItem)
            {
                item.SimulationSpeed(rate);
            }
        }
        public void Simulate(float t)
        {
            foreach (var item in particlesItem)
            {
                item.Simulate(t);
            }
        }
        public void Play()
        {
            foreach (var item in particlesItem)
            {
                item.Play();
            }
        }
    }
}

