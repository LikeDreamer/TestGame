//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace AltarOfSword
{
    public static class EffectExtension
    {
        public static float GetMaxValue(this ParticleSystem.MinMaxCurve minMaxCurve)
        {
            switch (minMaxCurve.mode)
            {
                case ParticleSystemCurveMode.Constant:
                    return minMaxCurve.constant;
                case ParticleSystemCurveMode.Curve:
                    return minMaxCurve.curveMultiplier;
                case ParticleSystemCurveMode.TwoConstants:
                    return Mathf.Max(minMaxCurve.constantMax, minMaxCurve.constantMin);
                case ParticleSystemCurveMode.TwoCurves:
                    return minMaxCurve.curveMultiplier;
            }
            return -1f;
        }

        public static float GetDuration(this ParticleSystem particle, bool allowLoop = false)
        {
            if (!particle.emission.enabled) return 0f;
            if (particle.main.loop && !allowLoop)
            {
                return -1f;
            }
            if (particle.emission.rateOverTimeMultiplier <= 0)
            {
                return particle.main.startDelay.GetMaxValue() + particle.main.startLifetime.GetMaxValue();
            }
            else
            {
                return particle.main.startDelay.GetMaxValue() + Mathf.Max(particle.main.duration, particle.main.startLifetime.GetMaxValue());
            }
        }

        public static float GetDuration(Transform transform, bool includeChildren = true, bool includeInactive = true, bool allowLoop = false)
        {
            if (includeChildren)
            {
                ParticleSystem[] particles = transform.GetComponentsInChildren<ParticleSystem>(includeInactive);
                float duration = -1f;
                for (int i = 0; i < particles.Length; i++)
                {
                    ParticleSystem ps = particles[i];
                    float time = ps.GetDuration(allowLoop);
                    if (time > duration)
                    {
                        duration = time;
                    }
                }
                return duration;
            }
            else
            {
                ParticleSystem ps = transform.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    return ps.GetDuration(allowLoop);
                }
                else
                {
                    return -1f;
                }
            }
        }
    }
}
