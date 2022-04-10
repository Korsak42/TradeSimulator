using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour, IMarket
{
    [SerializeField]
    public Warehouse Warehouse;

    public void Start()
    {
        TestInit();
    }
    public void TestInit()
    {
        Warehouse.TestInit();
    }
    public void BuyResource(Resource resource, double gold)
    {
        throw new System.NotImplementedException();
    }

    public float CalculatePrice(Resource resource, double needsAmount, double warehouseAmount)
    {
        var resourceList = DataKeeper.instance.GetResourceList();
        var returnValue = (float)(needsAmount / warehouseAmount * resourceList.GetDefaultPrice(resource.Name));
        return returnValue;
    }

    public float CalculatePrice(Resource resource, double amount)
    {
        var resourceList = DataKeeper.instance.GetResourceList();
        var returnValue = (float)(amount / Warehouse.GetAmount(resource.Name) * Mathf.Pow(resourceList.GetDefaultPrice(resource.Name), 2));
        return returnValue;
    }

    public float CalculateSellPrice(EnumResource.ResourceName resourceName, double amount)
    {
        var resourceList = DataKeeper.instance.GetResourceList();
        var returnValue = (float)(amount / Warehouse.GetAmount(resourceName) * Mathf.Pow(resourceList.GetDefaultPrice(resourceName), 2)) * 0.95f ;
        return returnValue;
    }
    public float CalculateBuyPrice(EnumResource.ResourceName resourceName, double amount)
    {
        var resourceList = DataKeeper.instance.GetResourceList();
        var returnValue = (float)(amount / Warehouse.GetAmount(resourceName) * Mathf.Pow(resourceList.GetDefaultPrice(resourceName), 2)) * 1.05f;
        return returnValue;
    }
}
