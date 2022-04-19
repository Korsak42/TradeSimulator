using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[ShowOdinSerializedPropertiesInInspector]
public class DemandModule : MonoBehaviour
{
    public Strat Strat;

    public Dictionary<Resource, double> demands = new();

    public double CurrentTurnGoldSpend;
    [Button]
    public void GlobalInit(Strat strat)
    {
        Strat = strat;
        List<Resource> resources = new List<Resource>(DataKeeper.instance.DemandsList.GetDemands(Strat.StratType));
        foreach(Resource resource in resources)
        {
            demands.Add(resource, resource.GetAmountToConsume(Strat));
        }
    }
    [Button]
    public int CalculateReserveDivider(double amountOnWarehouse, double amountDemands)
    {
        for (int i = 10; i > 0; i--)
        {
            if (amountOnWarehouse / (amountDemands * i) > 1)
                return i;
            else
                i--;
        }
        return 1;
    }
}
