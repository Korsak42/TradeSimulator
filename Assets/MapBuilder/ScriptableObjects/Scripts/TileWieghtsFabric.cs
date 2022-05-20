using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 class TileWieghtsFabric
{
    public static int GetTileWeight(EnumTerrain terrain)
    {
        switch(terrain)
        {
            case EnumTerrain.Meadow: return 5;
            case EnumTerrain.Barren: return 10;
            case EnumTerrain.Forest: return 20;
            case EnumTerrain.Hill: return 50;
            case EnumTerrain.Woods: return 65;
            case EnumTerrain.Marsh: return 75;
            case EnumTerrain.Highlands: return 80;
            case EnumTerrain.Mountain: return 100;
            case EnumTerrain.Coastline: return 1000;
            case EnumTerrain.Sea: return 1000;
        }

        Debug.LogError($"Not implemented {terrain} in TileWeightsFabric");
        return 0;
    }
}
