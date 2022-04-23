using System.Collections.Generic;

public class Bureaucrats : Serviceman
{
    public override void GlobalInit()
    {
        StratType = EnumStrats.Bureaucrats;
        base.GlobalInit();
    }
    public override void ServiceWork()
    {
        if (GetHappy() > 1)
        {
            ImproveInfrastructure();
        }
        else
        {
            Corruption(Settlement.GetStrats());
        }
    }

    public void ImproveInfrastructure()
    {
        Consume(ResourceFactory.CreateResource(EnumResource.ResourceName.BuildingsMaterial));
    }

    public void Corruption(List<Strat> strats)
    {
        var resource = ResourceFactory.CreateResource(EnumResource.ResourceName.BuildingsMaterial);
        var amountOfMaterialsToSold = Warehouse.GetAmount(resource) / 10;
        var totalCost = amountOfMaterialsToSold * Market.CalculateSellPrice(resource, amountOfMaterialsToSold) / 2;
        SellGood(resource, amountOfMaterialsToSold);
        Gold -= totalCost / 2;
    }
}
