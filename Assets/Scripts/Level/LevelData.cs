using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData_", menuName = "Level/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private List<ItemData> _itemList;
    public List<ItemData> ItemList { get { return _itemList; } }

    [SerializeField] private Enums.ObjectTypes _goalType;
    public Enums.ObjectTypes GoalType { get { return _goalType; } }

    [SerializeField] private int _goalAmount;
    public int GoalAmount { get { return _goalAmount; } }

    [SerializeField] private int _timerSeconds = 60;
    public int TimerSeconds { get { return _timerSeconds; } }

    [Serializable]
    public class ItemData
    {
        [SerializeField] private Enums.ObjectTypes _type;
        public Enums.ObjectTypes Type { get { return _type; } }

        [SerializeField] private int _amount = 6;
        public int Amount { get { return _amount; } }
    }
}