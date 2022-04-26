using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpritesData", order = 7)]
public class TilesFeatureData : ScriptableObject
{
    public List<Color> Colors;

    public List<Texture> MeadowSprites;
    public List<Texture> BarrensSprites;
    public List<Texture> ForestSprites;
    public List<Texture> MountainSprites;
    public List<Texture> HillSprites;

    public Texture GetRandomTileFeature(EnumTerrain terrain)
    {
        switch (terrain)
        {
            case EnumTerrain.Meadow:
                {
                    return GetRandomTileFeatureFromList(MeadowSprites);
                }
            case EnumTerrain.Barren:
                {
                    return GetRandomTileFeatureFromList(BarrensSprites);
                }
            case EnumTerrain.Mountain:
                {
                    return GetRandomTileFeatureFromList(MountainSprites);
                }
            case EnumTerrain.Forest:
                {
                    return GetRandomTileFeatureFromList(ForestSprites);
                }
            case EnumTerrain.Hill:
                {
                    return GetRandomTileFeatureFromList(HillSprites);
                }
        }
        return null;
    }

    public Color GetTileColor(EnumTerrain terrain)
    {
        switch (terrain)
        {
            case EnumTerrain.Meadow:
                {
                    return Colors[0];
                }
            case EnumTerrain.Barren:
                {
                    return Colors[1];
                }
            case EnumTerrain.Mountain:
                {
                    return Colors[2];
                }
            case EnumTerrain.Forest:
                {
                    return Colors[3];
                }
            case EnumTerrain.Hill:
                {
                    return Colors[3];
                }
        }
        return new Color();
    }
    public Color GetRandomColor()
    {
        return Colors[Random.Range(0, Colors.Count - 1)];
    }



    public Texture GetRandomTileFeatureFromList(List<Texture> sprites)
    {
        return sprites[Random.Range(0, sprites.Count)];
    }

}
