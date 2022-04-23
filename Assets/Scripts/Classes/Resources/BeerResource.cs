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
        var ratio = (float)(amountNeeded / amountConsumed);
        if (ratio > 1f)
        {
            consumer.ChangeHealth(DataKeeper.instance.Constants.MinFloatStep * ratio, false);
            consumer.ChangeHappy(DataKeeper.instance.Constants.MinFloatStep * ratio, false);
        }
        else
        {
            consumer.ChangeHealth(DataKeeper.instance.Constants.MinFloatStep * ratio, true);
            consumer.ChangeHappy(DataKeeper.instance.Constants.MinFloatStep * ratio, true);
        }
    }

    public override double GetAmountToConsume(Strat consumer)
    {
        var pop = consumer.GetPopulation();
        var consumerRate = consumer.GetConsumeRate();
        var returnValue = pop * consumerRate;
        return returnValue;
    }
}
