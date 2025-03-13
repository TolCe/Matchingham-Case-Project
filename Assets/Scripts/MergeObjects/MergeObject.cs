using DG.Tweening;
using UnityEngine;
using System.Threading.Tasks;

public class MergeObject : MonoBehaviour
{
    private MeshCollider _meshCollider;

    private Rigidbody _rb;

    public Enums.ObjectTypes ObjectType { get; private set; }

    public Tile AttachedTile { get; private set; }

    private void Awake()
    {
        _meshCollider = GetComponent<MeshCollider>();
        _rb = GetComponent<Rigidbody>();
    }
    private void OnDisable()
    {
        transform.DOKill();
    }

    public void Initialize(Enums.ObjectTypes itemType)
    {
        gameObject.SetActive(true);

        SetPosition(new Vector3(Random.Range(-2f, 2f), Random.Range(0f, 0.6f), Random.Range(-3f, 3f)));

        ObjectType = itemType;

        _rb.isKinematic = false;
        _meshCollider.enabled = true;
    }

    public void AttachToTile(Tile tile, float duration = 0f)
    {
        AttachedTile?.AttachItem(null);

        AttachedTile = tile;

        if (AttachedTile != null)
        {
            AttachedTile.AttachItem(this);

            SetPosition(AttachedTile.GetObjectContainerPosition(), duration);
        }
    }

    public void SetPosition(Vector3 pos, float time = 0f)
    {
        transform.DOKill();

        transform.DORotate(Vector3.zero, time);
        transform.DOMove(pos, time);
    }

    public void OnSelected()
    {
        _rb.isKinematic = true;
        _meshCollider.enabled = false;
    }
}