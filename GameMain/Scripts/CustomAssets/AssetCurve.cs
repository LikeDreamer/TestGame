using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AltarOfSword
{
    [CreateAssetMenu(menuName = "GameAsset/AssetCurve")]
    public class AssetCurve : ScriptableObject
    {
        public AnimationCurve[] curveInfos;

        public AnimationCurve this[int index]
        {
            get
            {
                if(index<0|| index>=curveInfos.Length)return null;
                return curveInfos[index];
            }
        }
    }

}
