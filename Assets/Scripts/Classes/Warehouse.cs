using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour, IWarehouse
{
    [SerializeField]
    private List<Resource> Resources = new List<Resource>();


    public void TestInit()
    {
        Resources = DataKeeper.instance.GetListOfResources();
        foreach (Resource res in Resources)
        {
            res.Amount = Random.Range(100, 1000);
        }
    }

    public void CreateNewInstance(EnumResource.ResourceName resourceName, double amount)
    {
        Resources.Add(DataKeeper.instance.GetResourceList().DeepCopy(resourceName, amount));
    }

    public void RemoveResourceInstance(EnumResource.ResourceName resourceName)
    {
        Resources.Remove(FindResource(resourceName));
    }

    public Resource FindResource(EnumResource.ResourceName resourceName)
    {
        Resource res = new Resource();
        foreach (Resource r in Resources)
        {
            if (r.Name == resourceName)
                res = r;
        }
        return res;
    }

    public double GetAmount(EnumResource.ResourceName resourceName)
    {
        return FindResource(resourceName).Amount;
    }

    public void ChangeAmount(EnumResource.ResourceName resourceName, double amount, bool isPositive)
    {
        var res = FindResource(resourceName);
        if (isPositive)
            res.Amount += amount;
        else
            res.Amount -= amount;
    }

    public bool CheckResourceOnWarehouseAmountMoreThan(EnumResource.ResourceName resourceName, double amount)
    {
        if (FindResource(resourceName).Amount >= amount)
            return true;
        else
            return false;
    }

    public List<Resource> GetWarehouse()
    {
        return Resources;
    }
}
