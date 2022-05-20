using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RoadBuilder : MonoBehaviour
{
    public RoadsTilesData RoadsData;
    public HexPathfinder Pathfinder;
    public HexGraph HexGraph;

    public void CreateRoadBetweenTwoSettlements(HexCell[] cells, HexCell settlementHex1, HexCell settlementHex2)
    {
        CreateRoadFromPath(HexGraph.CreatePath(cells, settlementHex1, settlementHex2));
    }

    public void CreateRoadFromPath(List<HexCell> cells)
    {
        for (int i = 0; i < cells.Count - 1; i++)
        {
            MarkRoadsInCell(cells[i], cells[i + 1]);
        }
        for(int i = 1; i < cells.Count - 1; i++)
        {
            PlaceRoad(cells[i]);
        }
    }

    public void MarkRoadsInCell(HexCell originCell, HexCell targetCell)
    {
        targetCell.AddRoad(targetCell.GetNeighborDirection(originCell));
        targetCell.UpdateWeight();
    }

    public void PlaceRoad(HexCell cell)
    {
        if (cell.TerrainFeature != null)
            cell.RoadsEnable();
    }

}
