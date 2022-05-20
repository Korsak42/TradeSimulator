using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

using Sirenix.OdinInspector;
public class HexGrid : MonoBehaviour
{
    public int cellCountX;
    public int cellCountZ;
    public int chunkCountX, chunkCountZ;

    public int RegionsCount;


    public HexCell CellPrefab;
    public HexGridChunk chunkPrefab;
    public SettlementsGraph SettlementsGraph;
    public RoadBuilder RoadBuilder;
    public MapRegions RegionsBuilder;
    public HexGraph HexGraph;
    HexGridChunk[] chunks;

    HexCell[] cells;

    public Canvas cellLabelPrefab;
    public HexMesh HexMesh;
    public TilesFeatureData TilesData;
    public HexPathfinder Pathfinder;


    int searchFrontierPhase;

    #region UI

    public Toggle DistanceToggle;
    public Button SaveButton;
    public Button LoadButton;

    #endregion UI


    public void Save(BinaryWriter writer)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Save(writer);
        }
    }

    public void Load(BinaryReader reader)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Load(reader);
        }
    }

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

        CreateRegions();
        SetWeights();

        HexGraph.Create(cells);

        HexMesh.Triangulate(cells);


        CreateSettlements();

        SettlementsGraph.CreateGraph();


        CreateRoads();


        CreateFillInCell();

        ReverseChunks();

    }

    [Button]
    public void CreateRoad(HexCell start, HexCell end)
    {
        RoadBuilder.CreateRoadFromPath(HexGraph.CreatePath(cells, start, end));
    }

    public List<HexCell> test(HexCell start, HexCell end)
    {
        return HexGraph.CreatePath(cells, start, end);
    }


    public void CreateRegions()
    {
        var randomCellIndex = 0;
        var values = Enum.GetValues(typeof(EnumTerrain));
        for (int i = 0; i < RegionsCount; i++)
        {
            int error = 0;
            var terrainRandom = (EnumTerrain)UnityEngine.Random.Range(0, values.Length - 3);
            do
            {
                randomCellIndex = UnityEngine.Random.Range(0, cells.Length - 1);
                error++;
            }
            while (cells[randomCellIndex].inRegion || error >= 999);
            if (RegionsCount == 1)
                RegionsBuilder.Create(cells[187], EnumTerrain.Mountain);
            else
                RegionsBuilder.Create(cells[randomCellIndex], terrainRandom);
        }

        foreach (Region region in RegionsBuilder.Regions)
        {
            ColorizeCells(region.Cells);
        }
    }

    public void ReverseChunks()
    {
        foreach (HexGridChunk chunk in chunks)
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
                RoadBuilder.CreateRoadBetweenTwoSettlements(cells, settlement1, settlement2);
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
        ColorCell(cell, TilesData.GetTileColor(cell.TerrainType));
    }

    public void ColorCell(HexCell cell, Color color)
    {
        cell.Color = color;
        HexMesh.Triangulate(cell);
    }

    public void ColorizeCells(List<HexCell> cells)
    {
        foreach(HexCell cell in cells)
        {
            ColorCell(cell, TilesData.GetTileColor(cell.TerrainType));
        }
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
    [Button]
    public void FindPath(HexCell fromCell, HexCell toCell)
    {
        
        Search(fromCell, toCell);
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
        for (int i = 1; i < path1.Count - 1; i++)
        {
            if (path1[i] != null && path2 != null)
                CreateLandInPath(Pathfinder.CreatePath(path1[i], path2[i]));
        }
    }

    public void SetTerrainType(HexCell cell)
    {
        var values = Enum.GetValues(typeof(EnumTerrain));
        cell.TerrainType = (EnumTerrain)UnityEngine.Random.Range(0, values.Length - 1);

        ColorCell(cell, TilesData.GetTileColor(cell.TerrainType));
    }

    public void SetTerrainTypeFromDouble(HexCell cell)
    {
        var random = (int)UnityEngine.Random.Range(1, 5);
        if (random >= 2)
        {
            cell.TerrainType = EnumTerrain.Meadow;
        }
        else
        {
            cell.TerrainType = EnumTerrain.Sea;
        }

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
        for (int i = cells.Length - 1; i >= 0; i--)
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

    public void SetWeights()
    {
        foreach (HexCell cell in cells)
        {
            cell.SetWeight();
        }
    }

    
    public void Search(HexCell fromCell, HexCell toCell)
    {
        Pathfinder.Clear();
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].Distance = int.MaxValue;
        }
        fromCell.Distance = 0;
        Pathfinder.Enqueue(fromCell);
        while (Pathfinder.Count > 0)
        {

            HexCell current = Pathfinder.Dequeue();
            if (current == toCell)
            {
                break;
            }
            for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
            {
                HexCell neighbor = current.GetNeighbor(d);
                if (neighbor == null)
                {
                    continue;
                }
                if (neighbor.TerrainType == EnumTerrain.Sea)
                {
                    continue;
                }
                if (neighbor.TerrainType == EnumTerrain.Coastline)
                {
                    continue;
                }
                if (neighbor.TerrainType == EnumTerrain.Mountain)
                {
                    continue;
                }
                int distance = current.Distance;
                distance = neighbor.Weight;
                if (current.HasRoadThroughEdge(d))
                {
                    distance += (int)(neighbor.Weight / 2);
                }
                else
                {
                    distance += neighbor.Weight;
                }
                if (neighbor.Distance == int.MaxValue)
                {

                    neighbor.Distance = distance;
                    neighbor.PathFrom = current;
                    neighbor.SearchHeuristic = neighbor.Coordinates.DistanceTo(toCell.Coordinates);
                    
                    Pathfinder.Enqueue(neighbor);


                }
                else if (distance < neighbor.Distance)
                {
                    int oldPriority = neighbor.SearchPriority;

                    neighbor.Distance = distance;
                    Pathfinder.Change(neighbor, oldPriority);

                }
                
            }

        }
    }
}
