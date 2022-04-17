using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponResource : Resource
{
    public WeaponResource()
    {
        Name = EnumResource.ResourceName.Weapon;
        BasePrice = DataKeeper.instance.GetDefaultAmount(EnumResource.ResourceName.Weapon);
        BaseAmount = DataKeeper.instance.GetDefaultPrice(EnumResource.ResourceName.Weapon);
    }

    public override void ConsumeBy(Strat consumer, double amountNeeded, double amountConsumed)
    {
        var ratio = (float)(amountNeeded / amountConsumed);
        if (ratio > 1f)
        {
            consumer.Settlement.ChangeCrimeRate(DataKeeper.instance.Constants.MinFloatStep * ratio, false);
            consumer.ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep * ratio, false);
        }
        else
        {
            consumer.Settlement.ChangeCrimeRate(DataKeeper.instance.Constants.MinFloatStep * ratio, true);
            consumer.ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep * ratio, true);
        }
    }

    public override double GetAmountToConsume(Strat consumer)
    {
        return consumer.Settlement.GetDefenseArea() * consumer.ProductivityRate;
    }
}
