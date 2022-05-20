using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class FeatureCell : MonoBehaviour
{
    public List<RawImage> Places;

    public void PlaceTextures(EnumTerrain terrainType)
    {
        int r = 0;
        foreach(RawImage image in Places)
        {
            r = Random.Range(1, 9);
            if (r > 4)
                PlaceTexture(terrainType, image);
            else
                DeactivatePlace(image);
        }
        
    }
    [Button]
    public Rect Test(RawImage image)
    {
        return image.GetPixelAdjustedRect();
    }

    public void SetPriority()
    {
        
        foreach(RawImage image in Places)
        {
            image.gameObject.transform.SetSiblingIndex(0);
        }
        for(int i = 0; i < Places.Count; i++)
        {
            if (Places[i] != null)
                for (int j = 0; j < Places.Count; j++)
                {
                    if (Places[j] != null)
                    {
                        var y1 = GetBottomPointOfTexture(Places[i]);
                        var y2 = GetBottomPointOfTexture(Places[j]);
                        if (y1 < y2)
                        {
                            var jIndex = Places[j].gameObject.transform.GetSiblingIndex();
                            var iIndex = Places[i].gameObject.transform.GetSiblingIndex();

                            Places[i].gameObject.transform.SetSiblingIndex(jIndex);
                            Places[j].gameObject.transform.SetSiblingIndex(iIndex);
                        }
                    }
                }
        }
    }
    [Button]
    private float GetBottomPointOfTexture(RawImage image)
    {
        return image.gameObject.transform.position.z +
             Mathf.Abs(image.GetPixelAdjustedRect().y) -
             Mathf.Abs(image.GetPixelAdjustedRect().height);
    }
    
    public void DisplacePlaces()
    {
        foreach(RawImage image in Places)
        {
            Displace(image);
        }
    }

    public void Displace(RawImage image)
    {
        var rX = Random.Range(-1, 1);
        var rY = Random.Range(-1, 1);
        var rect = image.uvRect;
        image.uvRect.Set(rect.x + rX, rect.y + rY, rect.width, rect.height);        
    }

    public void PlaceTexture(EnumTerrain terrainType, RawImage image)
    {
        var featureList = TerrainFeaturesData.instance.GetFeatureList(terrainType);
        if (featureList == null)
        {
            image.color = new Color(0, 0, 0, 0);
            return;
        }
            
        Texture texture = null;
        var errorCount = 0;
        do
        {
            texture = featureList.GetRandomTexture();
            errorCount++;
            if (errorCount > 100)
            {
                texture = featureList.GetRandomTexture();
                break;
            }
        }
        while (CheckTextureAlreadyInCell(texture));

        var textureSize = featureList.GetSize(texture);
        var sizeX = textureSize.Wieght;
        var sizeY = textureSize.Height;

        image.texture = texture;
        image.rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
    }

    public bool CheckTextureAlreadyInCell(Texture texture)
    {
        foreach(RawImage image in Places)
        {
            if(image.texture == texture)
            {
                return true;
            }
        }

        return false;
    }

    public void ActivatePlaces()
    {
        for(int i = 0; i < Places.Count; i++)
        {
            ActivatePlace(i);
        }    
    }

    public void DeactivatePlaces()
    {
        for (int i = 0; i < Places.Count; i++)
        {
            DeactivatePlace(i);
        }
    }

    public void ActivatePlace(int i)
    {
        Places[i].gameObject.SetActive(true);
    }
    
    public void DeactivatePlace(int i)
    {
        Places[i].gameObject.SetActive(false);
    }
    public void DeactivatePlace(RawImage image)
    {
        image.gameObject.SetActive(false);
    }
}
