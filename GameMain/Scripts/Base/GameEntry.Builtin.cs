using UnityEngine;
using UnityGameFramework.Runtime;

namespace AltarOfSword //AltarOfSword
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        /// <summary>
        /// 获取游戏基础组件。
        /// </summary>
        public static BaseComponent Base { private set; get; }

        /// <summary>
        /// 获取配置组件。
        /// </summary>
        public static ConfigComponent Config { private set; get; }
        /// <summary>
        /// 获取数据结点组件。
        /// </summary>
        public static DataNodeComponent DataNode { private set; get; }
        /// <summary>
        /// 获取数据表组件。
        /// </summary>
        public static DataTableComponent DataTable { private set; get; }
        /// <summary>
        /// 获取调试组件。
        /// </summary>
        public static DebuggerComponent Debugger { private set; get; }

        /// <summary>
        /// 获取下载组件。
        /// </summary>
        public static DownloadComponent Download { private set; get; }

        /// <summary>
        /// 获取实体组件。
        /// </summary>
        public static EntityComponent Entity { private set; get; }

        /// <summary>
        /// 获取事件组件。
        /// </summary>
        public static EventComponent Event { private set; get; }

        /// <summary>
        /// 获取文件系统组件。
        /// </summary>
        public static FileSystemComponent FileSystem { private set; get; }

        /// <summary>
        /// 获取有限状态机组件。
        /// </summary>
        public static FsmComponent Fsm { private set; get; }

        /// <summary>
        /// 获取本地化组件。
        /// </summary>
        public static LocalizationComponent Localization { private set; get; }

        /// <summary>
        /// 获取网络组件。
        /// </summary>
        public static NetworkComponent Network { private set; get; }


        /// <summary>
        /// 获取对象池组件。
        /// </summary>
        public static ObjectPoolComponent ObjectPool { private set; get; }


        /// <summary>
        /// 获取流程组件。
        /// </summary>
        public static ProcedureComponent Procedure { private set; get; }


        /// <summary>
        /// 获取资源组件。
        /// </summary>
        public static ResourceComponent Resource { private set; get; }

        /// <summary>
        /// 获取场景组件。
        /// </summary>
        public static SceneComponent Scene { private set; get; }

        /// <summary>
        /// 获取配置组件。
        /// </summary>
        public static SettingComponent Setting { private set; get; }

        /// <summary>
        /// 获取声音组件。
        /// </summary>
        public static SoundComponent Sound { private set; get; }
        /// <summary>
        /// 获取界面组件。
        /// </summary>
        public static UIComponent UI { private set; get; }

        /// <summary>
        /// 获取网络组件。
        /// </summary>
        public static WebRequestComponent WebRequest { private set; get; }

        private static void InitBuiltinComponents()
        {
            Base = UnityGameFramework.Runtime.GameEntry.GetComponent<BaseComponent>();
            Config = UnityGameFramework.Runtime.GameEntry.GetComponent<ConfigComponent>();
            DataNode = UnityGameFramework.Runtime.GameEntry.GetComponent<DataNodeComponent>();
            DataTable = UnityGameFramework.Runtime.GameEntry.GetComponent<DataTableComponent>();
            Debugger = UnityGameFramework.Runtime.GameEntry.GetComponent<DebuggerComponent>();
            Download = UnityGameFramework.Runtime.GameEntry.GetComponent<DownloadComponent>();
            Entity = UnityGameFramework.Runtime.GameEntry.GetComponent<EntityComponent>();
            Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();
            FileSystem = UnityGameFramework.Runtime.GameEntry.GetComponent<FileSystemComponent>();
            Fsm = UnityGameFramework.Runtime.GameEntry.GetComponent<FsmComponent>();
            Localization = UnityGameFramework.Runtime.GameEntry.GetComponent<LocalizationComponent>();
            Network = UnityGameFramework.Runtime.GameEntry.GetComponent<NetworkComponent>();
            ObjectPool = UnityGameFramework.Runtime.GameEntry.GetComponent<ObjectPoolComponent>();
            Procedure = UnityGameFramework.Runtime.GameEntry.GetComponent<ProcedureComponent>();
            Resource = UnityGameFramework.Runtime.GameEntry.GetComponent<ResourceComponent>();
            Scene = UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();
            Setting = UnityGameFramework.Runtime.GameEntry.GetComponent<SettingComponent>();
            Sound = UnityGameFramework.Runtime.GameEntry.GetComponent<SoundComponent>();
            UI = UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();
            WebRequest = UnityGameFramework.Runtime.GameEntry.GetComponent<WebRequestComponent>();
        }
    }
}
