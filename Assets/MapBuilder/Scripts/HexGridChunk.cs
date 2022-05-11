using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class HexGridChunk : MonoBehaviour
{
	HexCell[] cells;

	HexMesh hexMesh;
	Canvas gridCanvas;

	public TilesFeatureData TilesData;
	public RoadsTilesData RoadsData;

	public SettlementsGraph SettlementsGraph;
	public ChunkTerrainBuilder TerrainBuilder;
	


	void Awake()
	{
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();

		cells = new HexCell[HexMetrics.chunkSizeX * HexMetrics.chunkSizeZ];
	}

	void Start()
	{
		
	}

	public void AddCell(int index, HexCell cell)
	{
		cells[index] = cell;
		cell.transform.SetParent(transform, false);
	}

	public void CreateSettlement()
	{
		var cell = cells[UnityEngine.Random.Range(0, cells.Length - 1)];
		var neighbours = cell.GetNeighbors();
		foreach (HexCell c in neighbours)
		{
			if(c != null)
				c.ShouldBeEmpty = true;
		}
		var values = Enum.GetValues(typeof(EnumCityClasses));
		var settlementType = (EnumCityClasses)UnityEngine.Random.Range(0, values.Length - 1);
		SettlementFactory.CreateSettlement(settlementType, cell);
		cell.TerrainFeature.texture = TilesData.GetCityIcon(settlementType);
		cell.TerrainFeature.color = new Color(255, 255, 255, 255);
		cell.isCity = true;
		SettlementsGraph.AddSettlementToList(cell);
	}

	public void CreateTerrainFeature(HexCell cell, EnumTerrain terrain)
	{
		if (cell.isCity) return;

		cell.SetTerrainFeatures();
		//cell.TerrainFeature.color = new Color(255, 255, 255, 255);
	}

    internal void CreateTerrainFeatures()
    {
        foreach (HexCell c in cells)
        {
			CreateTerrainFeature(c, c.TerrainType);
        }			
    }

	public void ReverseCellOrderInInspector()
    {
		int j = 0;
		for(int i = cells.Length - 1; i >= 0; i--)
        {
			cells[i].gameObject.transform.SetSiblingIndex(j);
			
			j++;
        }
    }

}

