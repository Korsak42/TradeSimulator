using System.Collections.Generic;
using UnityEngine;

public class Beggars : Serviceman
{
    public override void GlobalInit()
    {
        StratType = EnumStrats.Beggars;
        base.GlobalInit();
    }
    public override void ServiceWork()
    {
        var amountToFind = DemandModule.demands[ResourceFactory.CreateResource(EnumResource.ResourceName.Leavings)];
        var amountFound = SearchForLeavings(Settlement.GetStrats(), amountToFind);
        if (amountToFind > amountFound)
        {
            Settlement.ChangeCrimeRate(DataKeeper.instance.Constants.MinFloatStep, false);
        }
    }

    public double SearchForLeavings(List<Strat> strats, double amountNeeded)
    {
        var resource = ResourceFactory.CreateResource(EnumResource.ResourceName.Leavings);
        double stolenLeavings = 0;
        foreach (IStrat strat in strats)
        {
            if (stolenLeavings < amountNeeded)
            {
                var warehouse = strat.GetWarehouse();
                if (warehouse.IsWarehouseHasThisValue(resource))
                {
                    stolenLeavings += Random.Range(0f, (float)Warehouse.GetAmount(resource));
                }
            }
        }
        return stolenLeavings;
    }
}
