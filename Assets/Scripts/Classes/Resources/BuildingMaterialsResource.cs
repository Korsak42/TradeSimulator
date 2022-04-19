using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMaterialsResource : Resource
{
    public BuildingMaterialsResource()
    {
        Name = EnumResource.ResourceName.BuildingsMaterial;
        BasePrice = DataKeeper.instance.GetDefaultAmount(EnumResource.ResourceName.BuildingsMaterial);
        BaseAmount = DataKeeper.instance.GetDefaultPrice(EnumResource.ResourceName.BuildingsMaterial);
    }
    public override void ConsumeBy(Strat consumer, double amountNeeded, double amountConsumed)
    {
        throw new System.NotImplementedException();
    }

    public override double GetAmountToConsume(Strat consumer)
    {
        throw new System.NotImplementedException();
    }
}
