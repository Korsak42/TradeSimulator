using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarket
{
    void GlobalInit();    
    float CalculatePrice(Resource resource, double needsAmount, double warehouseAmount);
    float CalculatePrice(Resource resource, double amount);
}
