using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRegions : MonoBehaviour
{
    public HexGrid grid;

	[Range(0, 10)]
	public int mapBorderX = 5;

	[Range(0, 10)]
	public int mapBorderZ = 5;

	int xMin, xMax, zMin, zMax;

	public void GenerateMap()
	{
		grid.CreateMap();


	}
}
