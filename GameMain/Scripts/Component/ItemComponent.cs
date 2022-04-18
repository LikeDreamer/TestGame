using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

//namespace AltarOfSword
//{
//    /// <summary>
//    /// ���������
//    /// </summary>
//    [DisallowMultipleComponent]
//    [AddComponentMenu("Game Framework/Item")]
//    public sealed partial class ItemComponent : GameFrameworkComponent
//    {
//        private const int DefaultPriority = 0;
//        private IItemManager m_ItemManager = null;
//        private EventComponent m_EventComponent = null;

//        private readonly List<IItem> m_InternalItemResults = new List<IItem>();

//        [SerializeField]
//        private bool m_EnableShowItemUpdateEvent = false;

//        [SerializeField]
//        private bool m_EnableShowItemDependencyAssetEvent = false;

//        [SerializeField]
//        private Transform m_InstanceRoot = null;

//        [SerializeField]
//        private string m_ItemHelperTypeName = "UnityGameFramework.Runtime.DefaultItemHelper";


//        [SerializeField]
//        private string m_ItemGroupHelperTypeName = "UnityGameFramework.Runtime.DefaultItemGroupHelper";



//        /// <summary>
//        /// ��ȡ����������
//        /// </summary>
//        public int ItemCount
//        {
//            get
//            {
//                return m_ItemManager.ItemCount;
//            }
//        }

//        /// <summary>
//        /// ��ȡ������������
//        /// </summary>
//        public int ItemGroupCount
//        {
//            get
//            {
//                return m_ItemManager.ItemGroupCount;
//            }
//        }

//        /// <summary>
//        /// ��Ϸ��������ʼ����
//        /// </summary>
//        protected override void Awake()
//        {
//            base.Awake();

//            m_ItemManager = GameFrameworkEntry.GetModule<IItemManager>();
//            if (m_ItemManager == null)
//            {
//                Log.Fatal("Item manager is invalid.");
//                return;
//            }

//            m_ItemManager.ShowItemSuccess += OnShowItemSuccess;
//            m_ItemManager.ShowItemFailure += OnShowItemFailure;

//            if (m_EnableShowItemUpdateEvent)
//            {
//                m_ItemManager.ShowItemUpdate += OnShowItemUpdate;
//            }

//            if (m_EnableShowItemDependencyAssetEvent)
//            {
//                m_ItemManager.ShowItemDependencyAsset += OnShowItemDependencyAsset;
//            }

//            m_ItemManager.HideItemComplete += OnHideItemComplete;
//        }

//        private void Start()
//        {
//            BaseComponent baseComponent = GameEntry.GetComponent<BaseComponent>();
//            if (baseComponent == null)
//            {
//                Log.Fatal("Base component is invalid.");
//                return;
//            }

//            m_EventComponent = GameEntry.GetComponent<EventComponent>();
//            if (m_EventComponent == null)
//            {
//                Log.Fatal("Event component is invalid.");
//                return;
//            }

//            if (baseComponent.EditorResourceMode)
//            {
//                m_ItemManager.SetResourceManager(baseComponent.EditorResourceHelper);
//            }
//            else
//            {
//                m_ItemManager.SetResourceManager(GameFrameworkEntry.GetModule<IResourceManager>());
//            }

//            m_ItemManager.SetObjectPoolManager(GameFrameworkEntry.GetModule<IObjectPoolManager>());

//            ItemHelperBase itemHelper = Helper.CreateHelper(m_ItemHelperTypeName, m_CustomItemHelper);
//            if (itemHelper == null)
//            {
//                Log.Error("Can not create item helper.");
//                return;
//            }

//            itemHelper.name = "Item Helper";
//            Transform transform = itemHelper.transform;
//            transform.SetParent(this.transform);
//            transform.localScale = Vector3.one;

//            m_ItemManager.SetItemHelper(itemHelper);

//            if (m_InstanceRoot == null)
//            {
//                m_InstanceRoot = new GameObject("Item Instances").transform;
//                m_InstanceRoot.SetParent(gameObject.transform);
//                m_InstanceRoot.localScale = Vector3.one;
//            }

//            for (int i = 0; i < m_ItemGroups.Length; i++)
//            {
//                if (!AddItemGroup(m_ItemGroups[i].Name, m_ItemGroups[i].InstanceAutoReleaseInterval, m_ItemGroups[i].InstanceCapacity, m_ItemGroups[i].InstanceExpireTime, m_ItemGroups[i].InstancePriority))
//                {
//                    Log.Warning("Add item group '{0}' failure.", m_ItemGroups[i].Name);
//                    continue;
//                }
//            }
//        }

//        /// <summary>
//        /// �Ƿ���������顣
//        /// </summary>
//        /// <param name="itemGroupName">���������ơ�</param>
//        /// <returns>�Ƿ���������顣</returns>
//        public bool HasItemGroup(string itemGroupName)
//        {
//            return m_ItemManager.HasItemGroup(itemGroupName);
//        }

//        /// <summary>
//        /// ��ȡ�����顣
//        /// </summary>
//        /// <param name="itemGroupName">���������ơ�</param>
//        /// <returns>Ҫ��ȡ�������顣</returns>
//        public IItemGroup GetItemGroup(string itemGroupName)
//        {
//            return m_ItemManager.GetItemGroup(itemGroupName);
//        }

//        /// <summary>
//        /// ��ȡ���������顣
//        /// </summary>
//        /// <returns>���������顣</returns>
//        public IItemGroup[] GetAllItemGroups()
//        {
//            return m_ItemManager.GetAllItemGroups();
//        }

//        /// <summary>
//        /// ��ȡ���������顣
//        /// </summary>
//        /// <param name="results">���������顣</param>
//        public void GetAllItemGroups(List<IItemGroup> results)
//        {
//            m_ItemManager.GetAllItemGroups(results);
//        }

//        /// <summary>
//        /// ���������顣
//        /// </summary>
//        /// <param name="itemGroupName">���������ơ�</param>
//        /// <param name="instanceAutoReleaseInterval">����ʵ��������Զ��ͷſ��ͷŶ���ļ��������</param>
//        /// <param name="instanceCapacity">����ʵ�������������</param>
//        /// <param name="instanceExpireTime">����ʵ������ض������������</param>
//        /// <param name="instancePriority">����ʵ������ص����ȼ���</param>
//        /// <returns>�Ƿ�����������ɹ���</returns>
//        public bool AddItemGroup(string itemGroupName, float instanceAutoReleaseInterval, int instanceCapacity, float instanceExpireTime, int instancePriority)
//        {
//            if (m_ItemManager.HasItemGroup(itemGroupName))
//            {
//                return false;
//            }

//            ItemGroupHelperBase itemGroupHelper = Helper.CreateHelper(m_ItemGroupHelperTypeName, m_CustomItemGroupHelper, ItemGroupCount);
//            if (itemGroupHelper == null)
//            {
//                Log.Error("Can not create item group helper.");
//                return false;
//            }

//            itemGroupHelper.name = Utility.Text.Format("Item Group - {0}", itemGroupName);
//            Transform transform = itemGroupHelper.transform;
//            transform.SetParent(m_InstanceRoot);
//            transform.localScale = Vector3.one;

//            return m_ItemManager.AddItemGroup(itemGroupName, instanceAutoReleaseInterval, instanceCapacity, instanceExpireTime, instancePriority, itemGroupHelper);
//        }

//        /// <summary>
//        /// �Ƿ�������塣
//        /// </summary>
//        /// <param name="itemId">�����š�</param>
//        /// <returns>�Ƿ�������塣</returns>
//        public bool HasItem(int itemId)
//        {
//            return m_ItemManager.HasItem(itemId);
//        }

//        /// <summary>
//        /// �Ƿ�������塣
//        /// </summary>
//        /// <param name="itemAssetName">������Դ���ơ�</param>
//        /// <returns>�Ƿ�������塣</returns>
//        public bool HasItem(string itemAssetName)
//        {
//            return m_ItemManager.HasItem(itemAssetName);
//        }

//        /// <summary>
//        /// ��ȡ���塣
//        /// </summary>
//        /// <param name="itemId">�����š�</param>
//        /// <returns>���塣</returns>
//        public Item GetItem(int itemId)
//        {
//            return (Item)m_ItemManager.GetItem(itemId);
//        }

//        /// <summary>
//        /// ��ȡ���塣
//        /// </summary>
//        /// <param name="itemAssetName">������Դ���ơ�</param>
//        /// <returns>Ҫ��ȡ�����塣</returns>
//        public Item GetItem(string itemAssetName)
//        {
//            return (Item)m_ItemManager.GetItem(itemAssetName);
//        }

//        /// <summary>
//        /// ��ȡ���塣
//        /// </summary>
//        /// <param name="itemAssetName">������Դ���ơ�</param>
//        /// <returns>Ҫ��ȡ�����塣</returns>
//        public Item[] GetItems(string itemAssetName)
//        {
//            IItem[] items = m_ItemManager.GetItems(itemAssetName);
//            Item[] itemImpls = new Item[items.Length];
//            for (int i = 0; i < items.Length; i++)
//            {
//                itemImpls[i] = (Item)items[i];
//            }

//            return itemImpls;
//        }

//        /// <summary>
//        /// ��ȡ���塣
//        /// </summary>
//        /// <param name="itemAssetName">������Դ���ơ�</param>
//        /// <param name="results">Ҫ��ȡ�����塣</param>
//        public void GetItems(string itemAssetName, List<Item> results)
//        {
//            if (results == null)
//            {
//                Log.Error("Results is invalid.");
//                return;
//            }

//            results.Clear();
//            m_ItemManager.GetItems(itemAssetName, m_InternalItemResults);
//            foreach (IItem item in m_InternalItemResults)
//            {
//                results.Add((Item)item);
//            }
//        }

//        /// <summary>
//        /// ��ȡ�����Ѽ��ص����塣
//        /// </summary>
//        /// <returns>�����Ѽ��ص����塣</returns>
//        public Item[] GetAllLoadedItems()
//        {
//            IItem[] items = m_ItemManager.GetAllLoadedItems();
//            Item[] itemImpls = new Item[items.Length];
//            for (int i = 0; i < items.Length; i++)
//            {
//                itemImpls[i] = (Item)items[i];
//            }

//            return itemImpls;
//        }

//        /// <summary>
//        /// ��ȡ�����Ѽ��ص����塣
//        /// </summary>
//        /// <param name="results">�����Ѽ��ص����塣</param>
//        public void GetAllLoadedItems(List<Item> results)
//        {
//            if (results == null)
//            {
//                Log.Error("Results is invalid.");
//                return;
//            }

//            results.Clear();
//            m_ItemManager.GetAllLoadedItems(m_InternalItemResults);
//            foreach (IItem item in m_InternalItemResults)
//            {
//                results.Add((Item)item);
//            }
//        }

//        /// <summary>
//        /// ��ȡ�������ڼ�������ı�š�
//        /// </summary>
//        /// <returns>�������ڼ�������ı�š�</returns>
//        public int[] GetAllLoadingItemIds()
//        {
//            return m_ItemManager.GetAllLoadingItemIds();
//        }

//        /// <summary>
//        /// ��ȡ�������ڼ�������ı�š�
//        /// </summary>
//        /// <param name="results">�������ڼ�������ı�š�</param>
//        public void GetAllLoadingItemIds(List<int> results)
//        {
//            m_ItemManager.GetAllLoadingItemIds(results);
//        }

//        /// <summary>
//        /// �Ƿ����ڼ������塣
//        /// </summary>
//        /// <param name="itemId">�����š�</param>
//        /// <returns>�Ƿ����ڼ������塣</returns>
//        public bool IsLoadingItem(int itemId)
//        {
//            return m_ItemManager.IsLoadingItem(itemId);
//        }

//        /// <summary>
//        /// �Ƿ��ǺϷ������塣
//        /// </summary>
//        /// <param name="item">���塣</param>
//        /// <returns>�����Ƿ�Ϸ���</returns>
//        public bool IsValidItem(Item item)
//        {
//            return m_ItemManager.IsValidItem(item);
//        }

//        /// <summary>
//        /// ��ʾ���塣
//        /// </summary>
//        /// <typeparam name="T">�����߼����͡�</typeparam>
//        /// <param name="itemId">�����š�</param>
//        /// <param name="itemAssetName">������Դ���ơ�</param>
//        /// <param name="itemGroupName">���������ơ�</param>
//        public void ShowItem<T>(int itemId, string itemAssetName, string itemGroupName) where T : ItemLogic
//        {
//            ShowItem(itemId, typeof(T), itemAssetName, itemGroupName, DefaultPriority, null);
//        }

//        /// <summary>
//        /// ��ʾ���塣
//        /// </summary>
//        /// <param name="itemId">�����š�</param>
//        /// <param name="itemLogicType">�����߼����͡�</param>
//        /// <param name="itemAssetName">������Դ���ơ�</param>
//        /// <param name="itemGroupName">���������ơ�</param>
//        public void ShowItem(int itemId, Type itemLogicType, string itemAssetName, string itemGroupName)
//        {
//            ShowItem(itemId, itemLogicType, itemAssetName, itemGroupName, DefaultPriority, null);
//        }

//        /// <summary>
//        /// ��ʾ���塣
//        /// </summary>
//        /// <typeparam name="T">�����߼����͡�</typeparam>
//        /// <param name="itemId">�����š�</param>
//        /// <param name="itemAssetName">������Դ���ơ�</param>
//        /// <param name="itemGroupName">���������ơ�</param>
//        /// <param name="priority">����������Դ�����ȼ���</param>
//        public void ShowItem<T>(int itemId, string itemAssetName, string itemGroupName, int priority) where T : ItemLogic
//        {
//            ShowItem(itemId, typeof(T), itemAssetName, itemGroupName, priority, null);
//        }

//        /// <summary>
//        /// ��ʾ���塣
//        /// </summary>
//        /// <param name="itemId">�����š�</param>
//        /// <param name="itemLogicType">�����߼����͡�</param>
//        /// <param name="itemAssetName">������Դ���ơ�</param>
//        /// <param name="itemGroupName">���������ơ�</param>
//        /// <param name="priority">����������Դ�����ȼ���</param>
//        public void ShowItem(int itemId, Type itemLogicType, string itemAssetName, string itemGroupName, int priority)
//        {
//            ShowItem(itemId, itemLogicType, itemAssetName, itemGroupName, priority, null);
//        }

//        /// <summary>
//        /// ��ʾ���塣
//        /// </summary>
//        /// <typeparam name="T">�����߼����͡�</typeparam>
//        /// <param name="itemId">�����š�</param>
//        /// <param name="itemAssetName">������Դ���ơ�</param>
//        /// <param name="itemGroupName">���������ơ�</param>
//        /// <param name="userData">�û��Զ������ݡ�</param>
//        public void ShowItem<T>(int itemId, string itemAssetName, string itemGroupName, object userData) where T : ItemLogic
//        {
//            ShowItem(itemId, typeof(T), itemAssetName, itemGroupName, DefaultPriority, userData);
//        }

//        /// <summary>
//        /// ��ʾ���塣
//        /// </summary>
//        /// <param name="itemId">�����š�</param>
//        /// <param name="itemLogicType">�����߼����͡�</param>
//        /// <param name="itemAssetName">������Դ���ơ�</param>
//        /// <param name="itemGroupName">���������ơ�</param>
//        /// <param name="userData">�û��Զ������ݡ�</param>
//        public void ShowItem(int itemId, Type itemLogicType, string itemAssetName, string itemGroupName, object userData)
//        {
//            ShowItem(itemId, itemLogicType, itemAssetName, itemGroupName, DefaultPriority, userData);
//        }

//        /// <summary>
//        /// ��ʾ���塣
//        /// </summary>
//        /// <typeparam name="T">�����߼����͡�</typeparam>
//        /// <param name="itemId">�����š�</param>
//        /// <param name="itemAssetName">������Դ���ơ�</param>
//        /// <param name="itemGroupName">���������ơ�</param>
//        /// <param name="priority">����������Դ�����ȼ���</param>
//        /// <param name="userData">�û��Զ������ݡ�</param>
//        public void ShowItem<T>(int itemId, string itemAssetName, string itemGroupName, int priority, object userData) where T : ItemLogic
//        {
//            ShowItem(itemId, typeof(T), itemAssetName, itemGroupName, priority, userData);
//        }

//        /// <summary>
//        /// ��ʾ���塣
//        /// </summary>
//        /// <param name="itemId">�����š�</param>
//        /// <param name="itemLogicType">�����߼����͡�</param>
//        /// <param name="itemAssetName">������Դ���ơ�</param>
//        /// <param name="itemGroupName">���������ơ�</param>
//        /// <param name="priority">����������Դ�����ȼ���</param>
//        /// <param name="userData">�û��Զ������ݡ�</param>
//        public void ShowItem(int itemId, Type itemLogicType, string itemAssetName, string itemGroupName, int priority, object userData)
//        {
//            m_ItemManager.ShowItem(itemId, itemAssetName, itemGroupName, priority, ShowItemInfo.Create(itemLogicType, userData));
//        }

//        /// <summary>
//        /// �������塣
//        /// </summary>
//        /// <param name="itemId">�����š�</param>
//        public void HideItem(int itemId)
//        {
//            m_ItemManager.HideItem(itemId);
//        }

//        /// <summary>
//        /// �������塣
//        /// </summary>
//        /// <param name="itemId">�����š�</param>
//        /// <param name="userData">�û��Զ������ݡ�</param>
//        public void HideItem(int itemId, object userData)
//        {
//            m_ItemManager.HideItem(itemId, userData);
//        }

//        /// <summary>
//        /// �������塣
//        /// </summary>
//        /// <param name="item">���塣</param>
//        public void HideItem(Item item)
//        {
//            m_ItemManager.HideItem(item);
//        }

//        /// <summary>
//        /// �������塣
//        /// </summary>
//        /// <param name="item">���塣</param>
//        /// <param name="userData">�û��Զ������ݡ�</param>
//        public void HideItem(Item item, object userData)
//        {
//            m_ItemManager.HideItem(item, userData);
//        }

//        /// <summary>
//        /// ���������Ѽ��ص����塣
//        /// </summary>
//        public void HideAllLoadedItems()
//        {
//            m_ItemManager.HideAllLoadedItems();
//        }

//        /// <summary>
//        /// ���������Ѽ��ص����塣
//        /// </summary>
//        /// <param name="userData">�û��Զ������ݡ�</param>
//        public void HideAllLoadedItems(object userData)
//        {
//            m_ItemManager.HideAllLoadedItems(userData);
//        }

//        /// <summary>
//        /// �����������ڼ��ص����塣
//        /// </summary>
//        public void HideAllLoadingItems()
//        {
//            m_ItemManager.HideAllLoadingItems();
//        }

//        /// <summary>
//        /// ���������Ƿ񱻼�����
//        /// </summary>
//        /// <param name="item">���塣</param>
//        /// <param name="locked">�����Ƿ񱻼�����</param>
//        public void SetItemInstanceLocked(Item item, bool locked)
//        {
//            if (item == null)
//            {
//                Log.Warning("Item is invalid.");
//                return;
//            }

//            IItemGroup itemGroup = item.ItemGroup;
//            if (itemGroup == null)
//            {
//                Log.Warning("Item group is invalid.");
//                return;
//            }

//            itemGroup.SetItemInstanceLocked(item.gameObject, locked);
//        }

//        /// <summary>
//        /// ������������ȼ���
//        /// </summary>
//        /// <param name="item">���塣</param>
//        /// <param name="priority">�������ȼ���</param>
//        public void SetInstancePriority(Item item, int priority)
//        {
//            if (item == null)
//            {
//                Log.Warning("Item is invalid.");
//                return;
//            }

//            IItemGroup itemGroup = item.ItemGroup;
//            if (itemGroup == null)
//            {
//                Log.Warning("Item group is invalid.");
//                return;
//            }

//            itemGroup.SetItemInstancePriority(item.gameObject, priority);
//        }

//        private void OnShowItemSuccess(object sender, GameFramework.Item.ShowItemSuccessEventArgs e)
//        {
//            m_EventComponent.Fire(this, ShowItemSuccessEventArgs.Create(e));
//        }

//        private void OnShowItemFailure(object sender, GameFramework.Item.ShowItemFailureEventArgs e)
//        {
//            Log.Warning("Show item failure, item id '{0}', asset name '{1}', item group name '{2}', error message '{3}'.", e.ItemId.ToString(), e.ItemAssetName, e.ItemGroupName, e.ErrorMessage);
//            m_EventComponent.Fire(this, ShowItemFailureEventArgs.Create(e));
//        }

//        private void OnShowItemUpdate(object sender, GameFramework.Item.ShowItemUpdateEventArgs e)
//        {
//            m_EventComponent.Fire(this, ShowItemUpdateEventArgs.Create(e));
//        }

//        private void OnShowItemDependencyAsset(object sender, GameFramework.Item.ShowItemDependencyAssetEventArgs e)
//        {
//            m_EventComponent.Fire(this, ShowItemDependencyAssetEventArgs.Create(e));
//        }

//        private void OnHideItemComplete(object sender, GameFramework.Item.HideItemCompleteEventArgs e)
//        {
//            m_EventComponent.Fire(this, HideItemCompleteEventArgs.Create(e));
//        }
//    }
//}
