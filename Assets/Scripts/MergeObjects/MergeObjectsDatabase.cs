using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MergeObjectsDatabase", menuName = "Merge Object/Merge Objects Database")]
public class MergeObjectsDatabase : ScriptableObject
{
    [SerializeField] private List<MergeObjectData> _dataList;
    public List<MergeObjectData> DataList { get { return _dataList; } }

    [SerializeField] private int _initialPoolSize;
    public int InitialPoolSize { get { return _initialPoolSize; } }
}