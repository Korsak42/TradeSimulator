using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class HexCell : MonoBehaviour
{
    public EnumTerrain TerrainType;
    public Canvas Canvas;
    public TMPro.TMP_Text Text;
    public HexCoordinates Coordinates;
    public List<GameObject> RoadsSprites;
    public FeatureCellSwitcher FeatureCellSwitcher;
    public int Weight;

    int distance;
    public int Distance
    {
        get
        {
            return distance;
        }
        set
        {
            distance = value;
        }
    }





    [SerializeField]
    HexCell[] neighbors;

    public Color Color;
    public RawImage TerrainFeature;

    public bool ShouldBeEmpty;
    public bool HasRoads
    {
        get
        {
            for (int i = 0; i < roads.Length; i++)
            {
                if (roads[i])
                {
                    return true;
                }
            }
            return false;
        }
        set
        {
            UpdateWeight();
        }
    }
    public bool isCity;
    public bool isWater;
    public bool inRegion;
    public bool isBorder;
    [SerializeField]
    bool[] roads;

    public HexCell PathFrom { get; set; }


    public int SearchPhase { get; set; }
    public int SearchHeuristic { get; set; }
    public int SearchPriority
    {
        get
        {
            return distance + SearchHeuristic;
        }
    }

    public HexCell NextWithSamePriority { get; set; }

    public void Save(BinaryWriter writer)
    {
        writer.Write((byte)TerrainType);
    }

    public void Load(BinaryReader reader)
    {
        TerrainType = (EnumTerrain)reader.ReadByte();
    }

    public void SetWeight(int weight)
    {
        Weight = weight;
    }

    public void UpdateWeight()
    {
        Weight = (int)Weight / 2;
    }

    public HexCell GetNeighbor(HexDirection direction)
    {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;

    }

    public HexCell[] GetNeighbors()
    {
        return neighbors;
    }

    public bool HasRoadThroughEdge(HexDirection direction)
    {
        return roads[(int)direction];
    }

    public bool[] GetRoads()
    {
        return roads;
    }

    public void RemoveRoads()
    {
        for (int i = 0; i < neighbors.Length; i++)
        {
            if (roads[i])
            {
                roads[i] = false;
                neighbors[i].roads[(int)((HexDirection)i).Opposite()] = false;
            }
        }
    }

    public void AddRoad(HexDirection direction)
    {
        if (!roads[(int)direction])
        {
            SetRoad((int)direction, true);
        }
    }
    void SetRoad(int index, bool state)
    {
        roads[index] = state;
        neighbors[index].roads[(int)((HexDirection)index).Opposite()] = state;
    }

    public HexDirection GetNeighborDirection(HexCell target)
    {
        for (int i = 0; i < neighbors.Length; i++)
        {
            if (neighbors[i] == target)
            {
                return (HexDirection)i;
            }
        }
        Debug.Log($"{this} cell doesn't have {target} as neighbor");
        return 0;
    }

    public void RoadsEnable()
    {
        if (TerrainType != EnumTerrain.Coastline || TerrainType != EnumTerrain.Sea)
        {
            for (int i = 0; i < roads.Length; i++)
            {
                if (roads[i])
                    RoadsSprites[i].SetActive(true);
            }
        }
    }

    public void SetTerrainFeatures()
    {
        FeatureCellSwitcher.SetFeatureCells();
    }

    public void SetOrderLayer(int i)
    {
        Canvas.sortingOrder = i;
    }

    public void SetWeight()
    {
        SetWeight(TileWieghtsFabric.GetTileWeight(TerrainType));
    }
}
