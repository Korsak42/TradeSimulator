using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWarehouse
{
    public void TestInit();
    void CreateNewInstance(Resource resource, double amount);
    void RemoveResourceInstance(Resource resource);
    double GetAmount(Resource resource);
    void ChangeAmount(Resource resource, double amount);
    Resource FindResource(Resource resource);
    bool CheckResourceOnWarehouseAmountMoreThan(Resource resource, double amount);
}
