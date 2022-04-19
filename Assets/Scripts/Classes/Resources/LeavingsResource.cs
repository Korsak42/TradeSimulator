
public class LeavingsResource : Resource
{
    public LeavingsResource()
    {
        Name = EnumResource.ResourceName.Leavings;
        BasePrice = DataKeeper.instance.GetDefaultAmount(EnumResource.ResourceName.Leavings);
        BaseAmount = DataKeeper.instance.GetDefaultPrice(EnumResource.ResourceName.Leavings);
    }

    public override void ConsumeBy(Strat consumer, double amountNeeded, double amountConsumed)
    {
        var ratio = (float)(amountNeeded / amountConsumed);
        if (ratio > 1f)
        {
            consumer.ChangeHealth(DataKeeper.instance.Constants.MinFloatStep * ratio, false);
            consumer.ChangeHappy(DataKeeper.instance.Constants.MinFloatStep, false);
            consumer.ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep, false);
        }
        else
        {
            consumer.ChangeHealth(DataKeeper.instance.Constants.MinFloatStep, true);
            consumer.ChangeHappy(DataKeeper.instance.Constants.MinFloatStep, true);
            consumer.ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep, true);
        }
    }

    public override double GetAmountToConsume(Strat consumer)
    {
        return consumer.GetPopulation() * consumer.GetConsumeRate();
    }
}
