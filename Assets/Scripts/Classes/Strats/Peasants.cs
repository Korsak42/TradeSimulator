

public class Peasants : ProduceStrat, IBuyer, ISeller
{
    private double CurrentTurnProfit;
    public override void GlobalInit()
    {
        StratType = EnumStrats.Peasants;
        base.GlobalInit();
    }
    public override void Produce()
    {
        base.Produce();
        var producedFoodAmount = UnityEngine.Random.Range(1, Settlement.GetFreeArea()) * ProductivityRate;
        Warehouse.ChangeAmount(ResourceFactory.CreateResource(EnumResource.ResourceName.Food), producedFoodAmount);
    }


}
