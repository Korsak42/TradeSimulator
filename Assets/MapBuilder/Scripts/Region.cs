using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    public EnumTerrain TerrainType;
    public List<HexCell> Cells = new List<HexCell>();

    public Region()
    {
        Cells = new List<HexCell>();
    }

    public void AddCell(HexCell cell)
    {
        Cells.Add(cell);
        cell.TerrainType = TerrainType;
        cell.inRegion = true;
    }

    public void SetTerrainType(EnumTerrain terrain)
    {
        TerrainType = terrain;
    }
}