using UnityEngine;
using Zenject;

public class TilesGenerator : MonoBehaviour
{
    [Inject] private TilesManager _tilesManager;

    [SerializeField] private TilesData _tileData;

    [SerializeField] private Transform _tileContainer;
    private ObjectPool<Tile> _tilePool;

    private void Awake()
    {
        CreatePool();
    }
    private void Start()
    {
        CreateTiles();
    }

    public void CreatePool()
    {
        _tilePool = new ObjectPool<Tile>(_tileData.TilePrefab, _tileData.TileAmount, _tileContainer);
    }

    private void CreateTiles()
    {
        float xOffset = (_tileData.TileAmount - 1) / 2f * (_tileData.CellSize + _tileData.Spacing);

        for (int i = 0; i < _tileData.TileAmount; i++)
        {
            float xPos = i * (1 + _tileData.Spacing) - xOffset;

            Tile tile = _tilePool.Get();

            tile.Initialize(new Tile.Specs() { pos = new Vector3(xPos, 0, 0), parent = _tileContainer, Index = i });

            _tilesManager.OnTileCreated(tile);
        }
    }
}
