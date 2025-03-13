using UnityEngine;

[CreateAssetMenu(fileName = "MergeObjectData_", menuName = "Merge Object/Merge Object Data")]
public class MergeObjectData : ScriptableObject
{
    [SerializeField] private MergeObject _prefab;
    public MergeObject Prefab { get { return _prefab; } }

    [SerializeField] private Sprite _icon;
    public Sprite Icon { get { return _icon; } }

    [SerializeField] private Enums.ObjectTypes _objectType;
    public Enums.ObjectTypes ObjectType { get { return _objectType; } }
}