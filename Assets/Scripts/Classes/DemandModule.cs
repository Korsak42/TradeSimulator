using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[ShowOdinSerializedPropertiesInInspector]
public class DemandModule : MonoBehaviour
{
    public Strat Strat;

    public Dictionary<Resource, double> demands = new();

    public void GlobalInit(Strat strat)
    {
        Strat = strat;
        List<Resource> resources = new List<Resource>(DataKeeper.instance.DemandsList.GetDemands(Strat.StratType));
        foreach(Resource resource in resources)
        {
            demands.Add(resource, resource.GetAmountToConsume(Strat));
        }
    }
}
