using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
[ShowOdinSerializedPropertiesInInspector]

public class Warehouse : MonoBehaviour, IWarehouse
{
    public Dictionary<Resource, double> Resources = new();
    [Button]
    public void TestInit()
    {
        var ResourceList = DataKeeper.instance.GetListOfResources();
        foreach (Resource res in ResourceList)
        {
            CreateNewInstance(res, Random.Range(100, 1000));
        }
    }


    public void CreateNewInstance(Resource resource, double amount)
    {
        Resources.Add(ResourceFactory.CreateResource(resource.Name), amount);
    }

    public void RemoveResourceInstance(Resource resource)
    {
        Resources.Remove(FindResource(resource));
    }

    public Resource FindResource(Resource resource)
    {

        return Resources.FirstOrDefault(x => x.Key == resource).Key;
    }
    [Button]
    public double GetAmount(Resource resource)
    {
        if (IsWarehouseHasThisValue(resource))
        {
            return Resources[FindResource(resource)];
        }
        else
        {
            return -1;
        }
    }

    public bool IsWarehouseHasThisValue(Resource resource)
    {
        foreach (KeyValuePair<Resource, double> res in Resources)
        {
            if (Equals(res.Key, resource))
                return true;
        }
        return false;
    }

    public void ChangeAmount(Resource resource, double amount)
    {
        if (IsWarehouseHasThisValue(resource))
        {
            Resources[FindResource(resource)] += amount;
        }
        else
        {
            if (amount > 0)
                CreateNewInstance(resource, amount);
            else
                throw new System.Exception($"Somebody trying to subtract from unexisted Resource {resource.Name}");
        }
    }

    public bool CheckResourceOnWarehouseAmountMoreThan(Resource resource, double amount)
    {
        if (Resources[FindResource(resource)] >= amount)
            return true;
        else
            return false;
    }

    [Button]
    public void Test()
    {
        foreach (KeyValuePair<Resource, double> pair in Resources)
        {
            Resources[pair.Key] = 100;
        }
    }
}
