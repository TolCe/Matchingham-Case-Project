using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Tile/Tile Data")]
public class TilesData : ScriptableObject
{
    [SerializeField] private Tile _tilePrefab;
    public Tile TilePrefab { get { return _tilePrefab; } }

    [SerializeField] private int _tileAmount = 7;
    public int TileAmount { get { return _tileAmount; } }

    [SerializeField] private float _spacing = 0.1f;
    public float Spacing { get { return _spacing; } }

    [SerializeField] private float _cellSize = 1f;
    public float CellSize { get { return _cellSize; } }
}