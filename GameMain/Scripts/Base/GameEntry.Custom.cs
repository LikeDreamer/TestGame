using UnityEngine;
using UnityGameFramework.Runtime;

namespace AltarOfSword
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
         //public static BuiltinDataComponent BuiltinData { private set; get; }
        //public static ItemComponent Item { private set; get; }
        public static SkillComponent Skill { private set; get; }

        private static void InitCustomComponents()
        {
            //BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            //Item = UnityGameFramework.Runtime.GameEntry.GetComponent<ItemComponent>();
            //Data = UnityGameFramework.Runtime.GameEntry.GetComponent<DataComponent>();
            Skill = UnityGameFramework.Runtime.GameEntry.GetComponent<SkillComponent>();
        }

       
    }
}
