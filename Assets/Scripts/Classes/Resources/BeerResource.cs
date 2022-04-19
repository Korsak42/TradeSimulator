using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerResource : Resource
{
    public BeerResource()
    {
        Name = EnumResource.ResourceName.Beer;
        BasePrice = DataKeeper.instance.GetDefaultAmount(EnumResource.ResourceName.Beer);
        BaseAmount = DataKeeper.instance.GetDefaultPrice(EnumResource.ResourceName.Beer);
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
