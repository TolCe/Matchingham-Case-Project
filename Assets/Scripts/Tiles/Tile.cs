using UnityEngine;

public class Tile : MonoBehaviour
{
    public Specs Spec { get; private set; }

    public MergeObject AttachedItem { get; private set; }

    [SerializeField] private Transform _objectContainer;

    public struct Specs
    {
        public Vector3 pos;
        public Transform parent;
        public int Index;
    }

    public void Initialize(Specs specs)
    {
        Spec = specs;

        transform.SetParent(Spec.parent, false);
        transform.localPosition = Spec.pos;

        gameObject.SetActive(true);
    }

    public void AttachItem(MergeObject item)
    {
        AttachedItem = item;
    }

    public Vector3 GetObjectContainerPosition()
    {
        return _objectContainer.position;
    }
}