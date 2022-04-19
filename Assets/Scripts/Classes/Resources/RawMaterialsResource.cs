using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterialsResource : Resource
{
    public RawMaterialsResource()
    {
        Name = EnumResource.ResourceName.RawMaterial;
        BasePrice = DataKeeper.instance.GetDefaultAmount(EnumResource.ResourceName.RawMaterial);
        BaseAmount = DataKeeper.instance.GetDefaultPrice(EnumResource.ResourceName.RawMaterial);
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
