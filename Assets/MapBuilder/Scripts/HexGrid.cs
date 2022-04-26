using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
public class HexGrid : MonoBehaviour
{
    public int Width = 6;
    public int Height = 6;

    public HexCell CellPrefab;

    HexCell[] cells;

    public Canvas cellLabelPrefab;
    public HexMesh HexMesh;
    public TilesFeatureData TilesData;


    private void Awake()
    {
        HexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[Height * Width];

        for (int z = 0, i = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    private void Start()
    {
        HexMesh.Triangulate(cells);
        HexMesh.PaintUV();
    }




    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(CellPrefab);

        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.Coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        SetTerrainType(cell);
        ColorCell(cell, TilesData.GetTileColor(cell.TerrainType));
        ChangeTerrainFeature(cell, cell.TerrainType);
        if (x > 0)
        {
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);
        }
        if (z > 0)
        {
            if ((z & 1) == 0)
            {
                cell.SetNeighbor(HexDirection.SE, cells[i - Width]);
                if (x > 0)
                {
                    cell.SetNeighbor(HexDirection.SW, cells[i - Width - 1]);
                }
            }
            else
            {
                cell.SetNeighbor(HexDirection.SW, cells[i - Width]);
                if (x < Width - 1)
                {
                    cell.SetNeighbor(HexDirection.SE, cells[i - Width + 1]);
                }
            }
        }
    }


    public void SetTerrainType(HexCell cell)
    {
        var values = Enum.GetValues(typeof(EnumTerrain));
        cell.TerrainType = (EnumTerrain)UnityEngine.Random.Range(0, values.Length - 1);
    }

    public void ColorCell(HexCell cell, Color color)
    {
        cell.Color = color;
        HexMesh.Triangulate(cell);
    }

    public void ChangeTerrainFeature(HexCell cell, EnumTerrain terrain)
    {
        cell.TerrainFeature.texture = TilesData.GetRandomTileFeature(terrain);
    }

    public void PaintUV()
    {

    }
}

