using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataKeeper : MonoBehaviour
{
    public static DataKeeper instance;
    [SerializeField]
    private ResourceList ResourceList;
    public Constants Constants;
    public CityStructureData CityStructureData;
    public DemandsList DemandsList;


    private void Awake()
    {
        instance = this;
    }

    public ResourceList GetResourceList()
    {
        return ResourceList;
    }

    public List<Resource> GetListOfResources()
    {
        var resourceDatas = new List<ResourceData>(ResourceList.Resources);
        List<Resource> returnList = new();
        foreach (ResourceData rd in resourceDatas)
        {
            returnList.Add(ResourceFactory.CreateResource(rd.Name));
        }
        return returnList;
    }

    public float GetDefaultPrice(EnumResource.ResourceName resourceName)
    {
        return ResourceList.GetDefaultPrice(resourceName);
    }
    public float GetDefaultAmount(EnumResource.ResourceName resourceName)
    {
        return ResourceList.GetDefaultPrice(resourceName);
    }

    public Dictionary<EnumCityStructure, int> GetCityStructure(EnumCityClasses cityClass)
    {
        Dictionary<EnumCityStructure, int> returnDictionary = new Dictionary<EnumCityStructure, int>(CityStructureData.GetDict(cityClass));
        return returnDictionary;
    }
}
