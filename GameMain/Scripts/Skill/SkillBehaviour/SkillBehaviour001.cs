using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AltarOfSword
{
    public class SkillBehaviour001 : SkillBehaviour //播放动画
    {
        //if (!skillUnit.GetDataset(out DatasetB1S1 dataset)) return;
        //SkeletonAnimation skeletonAnimation = widget.GetComponentInChildren<SkeletonAnimation>();
        //string animName = widget.RuntimeData.Animation[dataset.Data2];
        //skeletonAnimation.AnimationState.SetAnimation(0, animName, dataset.Data1);
        //widget.SlowFrame.Animation = skeletonAnimation;
        /// <summary>
        /// DatasetB2S1
        /// </summary>
        /// <param Data0="是否循环"></param>
        /// <param Data1="是否跳帧"></param>
        /// <param Data2="动画名称"></param>
        public override void Execute(ActorSkillLogic logic, FrameCounter counter,  DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetB2S1 dataset)) return;
            SkeletonAnimation skeletonAnimation = logic.RuntimeData.Animation.SkeletonAnimation;
            if (skeletonAnimation == null) return;
            Spine.Animation animation = skeletonAnimation?.SkeletonDataAsset?.GetSkeletonData(true)?.FindAnimation(dataset.Data2);
            if (animation == null) return;
            if (!dataset.Data1)
            {
                skeletonAnimation.skeleton.SetToSetupPose();
                skeletonAnimation.skeleton.SetBonesToSetupPose();
                skeletonAnimation.AnimationState.Apply(skeletonAnimation.Skeleton);
                skeletonAnimation.ClearState();
            }
            skeletonAnimation.AnimationState.SetAnimation(0, animation, dataset.Data0);
        }
    }
}
