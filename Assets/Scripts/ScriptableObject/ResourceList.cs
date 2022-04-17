using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ResourseList", order = 1)]
public class ResourceList : SerializedScriptableObject
{
    
    public List<ResourceData> Resources;

    public float GetDefaultPrice(EnumResource.ResourceName resourceName)
    {
        return FindResource(resourceName).BasePrice;
    }

    public float GetDefaultAmount(EnumResource.ResourceName resourceName)
    {
        return FindResource(resourceName).BaseAmount;
    }

    public ResourceData FindResource(EnumResource.ResourceName resourceName)
    {
        var returnRes = Resources.Where(x => x.Name == resourceName).FirstOrDefault();
        if (returnRes == null)
            Debug.Log("Didn't find: " + name);
        return returnRes;
    }


    public ResourceData GetRandomResource()
    {
        int r = Random.Range(0, Resources.Count - 1);
        return Resources[r];
    }


}
