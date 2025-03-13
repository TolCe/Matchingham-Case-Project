using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MergeObjectsPool : Singleton<MergeObjectsPool>
{
    [SerializeField] private MergeObjectsDatabase _objectDatabase;

    [SerializeField] private Transform _itemContainer;

    private Dictionary<Enums.ObjectTypes, ObjectPool<MergeObject>> _mergeObjectPools;

    protected override void Awake()
    {
        base.Awake();

        CreatePool();
    }

    public void CreatePool()
    {
        _mergeObjectPools = new Dictionary<Enums.ObjectTypes, ObjectPool<MergeObject>>();

        foreach (MergeObjectData data in _objectDatabase.DataList)
        {
            ObjectPool<MergeObject> pool = new ObjectPool<MergeObject>(data.Prefab, _objectDatabase.InitialPoolSize, _itemContainer);

            List<MergeObject> objectsList = pool.GetDisabledObjects();

            _mergeObjectPools.Add(data.ObjectType, pool);
        }
    }

    public void GenerateItems(LevelData levelData)
    {
        ReturnAllItemsToPool();

        for (int i = 0; i < levelData.ItemList.Count; i++)
        {
            for (int j = 0; j < levelData.ItemList[i].Amount; j++)
            {
                MergeObject obj = _mergeObjectPools[levelData.ItemList[i].Type].Get();

                obj.Initialize(levelData.ItemList[i].Type);
            }
        }
    }

    public void ReturnItemToPool(MergeObject item)
    {
        item.AttachToTile(null);
        _mergeObjectPools[item.ObjectType].Return(item);
    }

    private void ReturnAllItemsToPool()
    {
        foreach (Enums.ObjectTypes type in _mergeObjectPools.Keys)
        {
            List<MergeObject> activeItemList = _mergeObjectPools[type].GetActiveObjects();
            foreach (MergeObject item in activeItemList)
            {
                ReturnItemToPool(item);
            }
        }
    }

    public MergeObjectData GetItemDataByType(Enums.ObjectTypes type)
    {
        return _objectDatabase.DataList.First(x => x.ObjectType == type);
    }
}
