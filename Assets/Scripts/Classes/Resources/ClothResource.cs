
public class ClothResource : Resource
{
    public ClothResource()
    {
        Name = EnumResource.ResourceName.Cloth;
        BasePrice = DataKeeper.instance.GetDefaultAmount(EnumResource.ResourceName.Cloth);
        BaseAmount = DataKeeper.instance.GetDefaultPrice(EnumResource.ResourceName.Cloth);
    }

    public override void ConsumeBy(Strat consumer, double amountNeeded, double amountConsumed)
    {
        var ratio = (float)(amountNeeded / amountConsumed);
        if (ratio > 1f)
        {
            consumer.ChangeHealth(DataKeeper.instance.Constants.MinFloatStep * ratio, false);
            consumer.ChangeHappy(DataKeeper.instance.Constants.MinFloatStep * ratio, false);
            consumer.ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep * ratio, false);
        }
        else
        {
            consumer.ChangeHealth(DataKeeper.instance.Constants.MinFloatStep * ratio, true);
            consumer.ChangeHappy(DataKeeper.instance.Constants.MinFloatStep * ratio, true);
            consumer.ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep * ratio, true);
        }
    }

    public override double GetAmountToConsume(Strat consumer)
    {
        return consumer.GetPopulation() * consumer.GetConsumeRate();
    }
}
