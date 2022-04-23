public class FoodResource : Resource
{
    public FoodResource()
    {
        Name = EnumResource.ResourceName.Food;
        BasePrice = DataKeeper.instance.GetDefaultAmount(EnumResource.ResourceName.Food);
        BaseAmount = DataKeeper.instance.GetDefaultPrice(EnumResource.ResourceName.Food);
    }

    public override void ConsumeBy(Strat consumer, double amountNeeded, double amountConsumed)
    {
        var ratio = (float)(amountNeeded / amountConsumed);
        if (ratio > 1f)
        {
            consumer.ChangeHealth(DataKeeper.instance.Constants.MinFloatStep * ratio, false);
        }
        else
        {
            consumer.ChangeHealth(DataKeeper.instance.Constants.MinFloatStep, true);
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
