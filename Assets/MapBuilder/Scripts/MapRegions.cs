using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRegions : MonoBehaviour
{
    public int RegionSize;
    public GameObject RegionPrefab;
    public List<Region> Regions = new List<Region>();

    List<HexCell> dontTouchList = new List<HexCell>();
    List<HexCell> borderList = new List<HexCell>();

    public GameObject CreateRegionGameObject()
    {
        var go = Instantiate(RegionPrefab);
        go.transform.SetParent(this.transform);
        return go;
    }

    public void Create(HexCell startingCell, EnumTerrain terrain)
    {
        var randomBorder = 100 / RegionSize;
        int j = 0;
        var region = CreateRegionGameObject().GetComponent<Region>();
        Regions.Add(region);
        region.SetTerrainType(terrain);
        var selectedCell = startingCell;
        region.AddCell(startingCell);
        var internalList = new List<HexCell>();
        borderList.Add(startingCell);
        int random = 0;
        for (int i = 0; i <= RegionSize; i++)
        {
            if (borderList.Count != 0)
                borderList = GetAvailableNeigbors(borderList);
            else
                borderList = GetAvailableNeigbors(internalList);
            foreach (HexCell cell in borderList)
            {
                if (CheckCellInList(dontTouchList, cell)) continue;
                random = Random.Range(1, 100);
                if (random > randomBorder * i)
                {
                    region.AddCell(cell);
                    internalList.Add(cell);
                }
                else
                {
                    dontTouchList.Add(cell);
                }
            }

            borderList.Clear();
        }

        dontTouchList.Clear();
    }

    bool CheckCellInList(List<HexCell> cells, HexCell targetCell)
    {
        bool returnBool = false;
        foreach (HexCell cell in cells)
        {
            if (cell == targetCell)
                returnBool = true;
        }
        return returnBool;
    }

    List<HexCell> GetAvailableNeigbors(List<HexCell> targetCells)
    {
        var returnList = new List<HexCell>();
        for (int i = 0; i < targetCells.Count; i++)
        {
            foreach (HexCell cell in targetCells[i].GetNeighbors())
            {
                if (cell == null) break;
                if (cell.inRegion) break;

                returnList.Add(cell);
            }
        }

        return returnList;
    }
    void MarkCellsAsRegion(Region region, int i)
    {
        int random = 0;
    }
}
