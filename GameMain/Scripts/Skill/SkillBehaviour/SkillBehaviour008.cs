using UnityEngine;

namespace AltarOfSword
{
    public class SkillBehaviour008 : SkillBehaviour //播放音效
    {
        public override void Execute(ActorSkillLogic logic, FrameCounter counter, DataUnit dataUnit)
        {
            if (!dataUnit.GetDataset(out DatasetI1 dataset)) return;
            Vector3 position = logic.transform.position;
            position.y += logic.Rigidbody.SiteY / 2.0f;
            GameEntry.Sound.PlaySound(dataset.Data0, position);
        }
    }
}
