using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolResource : Resource
{
    public ToolResource()
    {
        Name = EnumResource.ResourceName.Tools;
        BasePrice = DataKeeper.instance.GetDefaultAmount(EnumResource.ResourceName.Tools);
        BaseAmount = DataKeeper.instance.GetDefaultPrice(EnumResource.ResourceName.Tools);
    }

    public override void ConsumeBy(Strat consumer, double amountNeeded, double amountConsumed)
    {
        var ratio = (float)(amountNeeded / amountConsumed);
        if (ratio > 1f)
        {
            consumer.ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep * ratio, false);
        }
        else
        {
            consumer.ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep * ratio, true);
        }
    }

    public override double GetAmountToConsume(Strat consumer)
    {
        return consumer.Settlement.GetWorkArea() * consumer.ProductivityRate;
    }
}
