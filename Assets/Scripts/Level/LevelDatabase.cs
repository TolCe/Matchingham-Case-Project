using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Level/Level Database")]
public class LevelDatabase : ScriptableObject
{
    [SerializeField] private List<LevelData> _levelDataList;
    public List<LevelData> LevelDataList { get { return _levelDataList; } }
}