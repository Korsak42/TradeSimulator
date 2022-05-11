using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainFeaturesData : MonoBehaviour
{
    public static TerrainFeaturesData instance;

    public TerrainFeaturesTextures MeadowFeatures;
    public TerrainFeaturesTextures BarrensFeatures;
    public TerrainFeaturesTextures ForestFeatures;
    public TerrainFeaturesTextures WoodsFeatures;
    public TerrainFeaturesTextures MarshesFeatures;
    public TerrainFeaturesTextures HillsFeatures;
    public TerrainFeaturesTextures MountainFeatures;
    private void Awake()
    {
        instance = this;    
    }

    public TerrainFeaturesTextures GetFeatureList(EnumTerrain terrain)
    {
        switch(terrain)
        {
            case EnumTerrain.Barren:
                return BarrensFeatures;
            case EnumTerrain.Forest:
                return ForestFeatures;
            case EnumTerrain.Meadow:
                return MeadowFeatures;
            case EnumTerrain.Mountain:
                return MountainFeatures;
            case EnumTerrain.Highlands:
                return null;
            case EnumTerrain.Marsh:
                return MarshesFeatures;
            case EnumTerrain.Hill:
                return HillsFeatures;
            case EnumTerrain.Sea:
                return null;
            case EnumTerrain.Coastline:
                return null;
            case EnumTerrain.Woods:
                return WoodsFeatures;
        }
        Debug.LogError($"{terrain} not implemented");
        return null;
    }

}
