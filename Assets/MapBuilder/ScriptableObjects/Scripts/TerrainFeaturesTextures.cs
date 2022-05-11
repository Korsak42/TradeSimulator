using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TerrainFeaturesSprites", order = 9)]
public class TerrainFeaturesTextures : SerializedScriptableObject
{
    public class TextureSize
    {
        public int Height;
        public int Wieght;
    }

    public Dictionary<Texture, TextureSize> FeatureTextures;

    public TextureSize GetSize(Texture texture)
    {
        return FeatureTextures[texture];
    }

    public Texture GetRandomTexture()
    {
        int r = Random.Range(0, FeatureTextures.Count - 1);
        var t = FeatureTextures.ElementAt(r).Key;
        return t;
    }
}
