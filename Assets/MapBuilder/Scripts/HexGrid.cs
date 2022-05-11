using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

using Sirenix.OdinInspector;
public class HexGrid : MonoBehaviour
{
    public int cellCountX;
    public int cellCountZ;
    public int chunkCountX, chunkCountZ;


    public HexCell CellPrefab;
    public HexGridChunk chunkPrefab;
    public SettlementsGraph SettlementsGraph;
    public RoadBuilder RoadBuilder;

    HexGridChunk[] chunks;

    HexCell[] cells;

    public Canvas cellLabelPrefab;
    public HexMesh HexMesh;
    public TilesFeatureData TilesData;
    public HexPathfinder Pathfinder;


    int searchFrontierPhase;

    void Search(HexCell fromCell, HexCell toCell, int speed)
    {
        searchFrontierPhase += 2;
        fromCell.SearchPhase = searchFrontierPhase;
        fromCell.Distance = 0;
        Pathfinder.Enqueue(fromCell);

    }


    private void Awake()
    {
        HexMesh = GetComponentInChildren<HexMesh>();

        cellCountX = chunkCountX * HexMetrics.chunkSizeX;
        cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;

        CreateMap();
    }

    public void CreateMap()
    {
        CreateChunks();

        CreateCells();
        ReverseCellSortingLayer();
        //CreateLand(4);

        CreateSettlements();

        SettlementsGraph.CreateGraph();

        CreateRoads();

        CreateFillInCell();

        ReverseChunks();
    }

    public void ReverseChunks()
    {
        foreach(HexGridChunk chunk in chunks)
        {
            chunk.ReverseCellOrderInInspector();
        }
    }

    public HexCell GetCell(int xOffset, int zOffset)
    {
        return cells[xOffset + zOffset * cellCountX];
    }

    public HexCell GetOffsetCell(HexCell cell, int xOffset, int zOffset)
    {
        return cells[cell.Coordinates.X + xOffset + cell.Coordinates.Z + zOffset * cellCountX];
    }

    public HexCell GetCell(int cellIndex)
    {
        return cells[cellIndex];
    }


    [Button]
    public HexDirection Test(HexCell cell1, HexCell cell2)
    {
        return cell1.GetNeighborDirection(cell2);
    }

    

    private void CreateRoads()
    {
        var SR = SettlementsGraph.GetNextConnectedSettlements();
        var settlement1 = SR.Settlement1;
        var settlement2 = SR.Settlement2;
        do
        {
            if (settlement1 != null && settlement2 != null)
                RoadBuilder.CreateRoadBetweenTwoSettlements(settlement1, settlement2);
            SR = SettlementsGraph.GetNextConnectedSettlements();
            settlement1 = SR.Settlement1;
            settlement2 = SR.Settlement2;
        }
        while (settlement1 != null || settlement2 != null);
    }

    private void Start()
    {

        HexMesh.Triangulate(cells);
        HexMesh.PaintUV();
    }
    void CreateChunks()
    {
        chunks = new HexGridChunk[chunkCountX * chunkCountZ];

        for (int z = 0, i = 0; z < chunkCountZ; z++)
        {
            for (int x = 0; x < chunkCountX; x++)
            {
                HexGridChunk chunk = chunks[i++] = Instantiate(chunkPrefab);
                chunk.transform.SetParent(transform);
            }
        }
    }

    void CreateCells()
    {
        cells = new HexCell[cellCountZ * cellCountX];

        for (int z = 0, i = 0; z < cellCountZ; z++)
        {
            for (int x = 0; x < cellCountX; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(CellPrefab);

        cell.transform.localPosition = position;
        cell.Coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        SetInitialTerrainType(cell);
        cell.SetOrderLayer(i);
        ColorCell(cell, TilesData.GetTileColor(cell.TerrainType));
        AddCellToChunk(x, z, cell);
    
        if (x > 0)
        {
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);
        }
        if (z > 0)
        {
            if ((z & 1) == 0)
            {
                cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX]);
                if (x > 0)
                {
                    cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX - 1]);
                }
            }
            else
            {
                cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX]);
                if (x < cellCountX - 1)
                {
                    cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX + 1]);
                }
            }
        }
    }


    void AddCellToChunk(int x, int z, HexCell cell)
    {
        int chunkX = x / HexMetrics.chunkSizeX;
        int chunkZ = z / HexMetrics.chunkSizeZ;
        HexGridChunk chunk = chunks[chunkX + chunkZ * chunkCountX];

        int localX = x - chunkX * HexMetrics.chunkSizeX;
        int localZ = z - chunkZ * HexMetrics.chunkSizeZ;
        chunk.AddCell(localX + localZ * HexMetrics.chunkSizeX, cell);
    }

    public void SetInitialTerrainType(HexCell cell)
    {
        cell.TerrainType = EnumTerrain.Meadow;
    }

    public void ColorCell(HexCell cell, Color color)
    {
        cell.Color = color;
        HexMesh.Triangulate(cell);
    }



    public void PaintUV()
    {

    }

    public void CreateFillInCell()
    {
        foreach (HexGridChunk chunk in chunks)
        {
            chunk.CreateTerrainFeatures(); 
        }
        
    }

    public void CreateSettlements()
    {
        foreach (HexGridChunk chunk in chunks)
        {
            chunk.CreateSettlement();
        }
    }

    public void FindDistancesTo(HexCell cell)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Distance =
                cell.Coordinates.DistanceTo(cells[i].Coordinates);
        }
    }
    [Button]
    public void CreateLand(int index)
    {
        var RandomCell1 = GetCell(index);
        var RandomCell2 = GetCell(0, 2);
        var RandomCell3 = GetCell(2, 0);
        var RandomCell4 = GetCell(0, 2);
        var path1 = Pathfinder.CreatePath(RandomCell1, RandomCell2);
        var path2 = Pathfinder.CreatePath(RandomCell2, RandomCell3);
        var path3 = Pathfinder.CreatePath(RandomCell3, RandomCell4);
        var path4 = Pathfinder.CreatePath(RandomCell4, RandomCell1);

        CreateLandInPath(path1);
        CreateLandInPath(path2);
        CreateLandInPath(path3);
        CreateLandInPath(path4);

        ConnectTwoPathes(path1, path4);
        HexMesh.Triangulate(cells);
        HexMesh.PaintUV();
    }

    public void ConnectTwoPathes(List<HexCell> path1, List<HexCell> path2)
    {
        for(int i = 1; i < path1.Count - 1; i++)
        {
            if (path1[i] != null && path2 != null)
                CreateLandInPath(Pathfinder.CreatePath(path1[i], path2[i]));  
        }
    }

    public void SetTerrainType(HexCell cell)
    {
        var values = Enum.GetValues(typeof(EnumTerrain));
        cell.TerrainType = (EnumTerrain)UnityEngine.Random.Range(0, values.Length - 2);

        ColorCell(cell, TilesData.GetTileColor(cell.TerrainType));
    }


    public void CreateLandInPath(List<HexCell> cells)
    {
        foreach (HexCell cell in cells)
        {
            cell.TerrainType = EnumTerrain.Mountain;
            ColorCell(cell, TilesData.GetTileColor(cell.TerrainType));
        }    
    }


    public void ReverseCellSortingLayer()
    {
        int j = 0;
        for(int i = cells.Length - 1; i >= 0; i--)
        {
            cells[i].SetOrderLayer(j);
            j++;
        }
    }
    [Button]
    public void SetPriority(HexCell cell)
    {
        cell.FeatureCellSwitcher.SetPriority();
    }
}

