using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class MergeManager
{
    [Inject] private TilesManager _tilesManager;

    [Inject] private SuccessChecker _successChecker;

    public void CheckForMerge()
    {
        List<MergeObject> itemsToCheck = new List<MergeObject>();

        foreach (Tile tile in _tilesManager.TileList)
        {
            if (tile.AttachedItem != null)
            {
                itemsToCheck.Add(tile.AttachedItem);
            }
        }

        if (itemsToCheck.Count <= 2)
        {
            return;
        }

        List<MergeObject> mergedItems = new List<MergeObject>() { itemsToCheck[0] };

        for (int i = 1; i < itemsToCheck.Count; i++)
        {
            if (itemsToCheck[i].ObjectType == itemsToCheck[i - 1].ObjectType)
            {
                mergedItems.Add(itemsToCheck[i]);

                if (mergedItems.Count >= 3)
                {
                    MergeAnimation(new List<MergeObject>(mergedItems));
                    MergeItems(mergedItems);

                    break;
                }
            }
            else
            {
                mergedItems = new List<MergeObject>() { itemsToCheck[i] };
            }
        }
    }

    private void MergeItems(List<MergeObject> itemList)
    {
        foreach (MergeObject item in itemList)
        {
            _successChecker.OnMerge(item);
            MergeObjectsPool.Instance.ReturnItemToPool(item);
        }

        _tilesManager.TidyTiles();
    }

    private void MergeAnimation(List<MergeObject> mergedList)
    {
        Vector3 midPoint = 0.5f * (mergedList[0].transform.position + mergedList[^1].transform.position);

        for (int i = 0; i < mergedList.Count; i++)
        {
            mergedList[i].SetPosition(midPoint, 0.1f);
        }
    }
}