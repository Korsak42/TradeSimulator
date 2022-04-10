using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarket
{
    void BuyResource(Resource resource, double gold);
    
    float CalculatePrice(Resource resource, double needsAmount, double warehouseAmount);
    float CalculatePrice(Resource resource, double amount);
}
