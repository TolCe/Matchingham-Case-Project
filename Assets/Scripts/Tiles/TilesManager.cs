using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class TilesManager
{
    [Inject] private MergeManager _mergeManager;

    [Inject] private SignalBus _signalBus;

    public List<Tile> TileList { get; private set; }

    public TilesManager()
    {
        TileList = new List<Tile>();
    }

    public void OnTileCreated(Tile tile)
    {
        TileList.Add(tile);
    }

    public void OnObjectSelected(MergeObject obj)
    {
        PlaceItem(obj);
    }

    private async void PlaceItem(MergeObject obj)
    {
        Tile tile = FindFirstSuitableTile(obj);

        await CreateEmptyTileOnMiddle(tile);

        await obj.AttachToTile(tile, 0.4f);

        _mergeManager.CheckForMerge();

        if (CheckIfAllTilesFilled())
        {
            _signalBus.Fire(new GameSignals.CallLevelEnd(false));
        }
    }

    public Tile FindFirstSuitableTile(MergeObject item)
    {
        Tile suitedTile = null;
        bool previousMatched = false;

        foreach (Tile tile in TileList)
        {
            if (tile.AttachedItem == null)
            {
                suitedTile = tile;

                break;
            }

            if (item.ObjectType == tile.AttachedItem.ObjectType)
            {
                previousMatched = true;
            }
            else
            {
                if (previousMatched)
                {
                    suitedTile = tile;

                    break;
                }

                previousMatched = false;
            }
        }

        return suitedTile;
    }

    private async Task CreateEmptyTileOnMiddle(Tile tile)
    {
        if (tile.AttachedItem == null)
        {
            return;
        }

        if (tile != TileList[TileList.Count - 1])
        {
            for (int i = TileList.Count - 2; i >= 0; i--)
            {
                if (TileList[i].AttachedItem != null && TileList[i + 1].AttachedItem == null)
                {
                    await TileList[i].AttachedItem.AttachToTile(TileList[i + 1], 0.02f);
                }

                if (TileList[i] == tile)
                {
                    break;
                }
            }
        }
    }

    private bool CheckIfAllTilesFilled()
    {
        foreach (Tile tile in TileList)
        {
            if (tile.AttachedItem == null)
            {
                return false;
            }
        }

        return true;
    }

    //Move other items to empty tiles if some items merged
    public void TidyTiles()
    {
        foreach (Tile tile in TileList)
        {
            if (tile.AttachedItem != null)
            {
                Tile freeTile = FindFirstSuitableTile(tile.AttachedItem);

                if (freeTile.Spec.Index < tile.Spec.Index)
                {
                    PlaceItem(tile.AttachedItem);
                }
            }
        }
    }
}