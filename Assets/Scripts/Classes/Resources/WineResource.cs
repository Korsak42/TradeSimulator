using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineResource : Resource
{
    public WineResource()
    {
        Name = EnumResource.ResourceName.Wine;
        BasePrice = DataKeeper.instance.GetDefaultAmount(EnumResource.ResourceName.Wine);
        BaseAmount = DataKeeper.instance.GetDefaultPrice(EnumResource.ResourceName.Wine);
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
