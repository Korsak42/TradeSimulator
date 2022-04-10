using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWarehouse
{
    public void TestInit();
    void CreateNewInstance(EnumResource.ResourceName resourceName, double amount);
    void RemoveResourceInstance(EnumResource.ResourceName resourceName);
    double GetAmount(EnumResource.ResourceName resourceName);
    void ChangeAmount(EnumResource.ResourceName resourceName, double amount, bool isPositive);
    Resource FindResource(EnumResource.ResourceName resourceName);
    bool CheckResourceOnWarehouseAmountMoreThan(EnumResource.ResourceName resourceName, double amount);
    List<Resource> GetWarehouse();
}
