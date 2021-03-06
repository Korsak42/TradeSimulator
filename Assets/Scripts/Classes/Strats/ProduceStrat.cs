using Sirenix.OdinInspector;
using System;
using UnityEngine;
public class ProduceStrat : Strat, IProducer
{
    public Resource ProductionResource;

    private void Awake()
    {
        GlobalLinkStrat();
        SubsribeProducer();
    }
    [Button]
    public void ProducerGlobalInit()
    {
        SetProductionResource();
    }
    [Button]
    public void ProduceCycle()
    {
        Produce();
    }

    [Button]
    public double CalculateProducedResource()
    {
        var toolsAmount = Warehouse.GetAmount(ResourceFactory.CreateResource(EnumResource.ResourceName.Tools));
        var productionSquare = Settlement.GetWorkArea();
        if (productionSquare > toolsAmount)
            return toolsAmount * 2 * ProductivityRate;
        else
            return productionSquare * 2 * ProductivityRate;
    }

    public Resource GetProductionResource()
    {
        return ProductionResource;
    }
    [Button]
    public virtual void Produce()
    {
        var productedResourceAmount = CalculateProducedResource();
        Warehouse.ChangeAmount(ProductionResource, productedResourceAmount);
    }
    [Button]
    public void SetProductionResource()
    {
        ProductionResource = ResourceFactory.CreateResource(EnumResource.ResourceName.Food);
    }

    public void SubsribeProducer()
    {
        GlobalData.instance.SubsribeProducer(this);
    }
    [Button]
    public void SellProductedResource()
    {
        if (DemandModule.demands.ContainsKey(ProductionResource))
        {
            var amountOfDemands = DemandModule.demands[ProductionResource];
            var divider = DemandModule.CalculateReserveDivider(Warehouse.GetAmount(ProductionResource), amountOfDemands);
            var amountToSold = Math.Min(amountOfDemands * divider, Warehouse.GetAmount(ProductionResource));
            PayTaxes(amountToSold);
            SellGood(ProductionResource, amountToSold);
        }
        else
        {
            SellGood(ProductionResource, Warehouse.GetAmount(ProductionResource));
        }
    }

    public void PayTaxes(double amountToSold)
    {
        var tax = (Market.CalculateSellPrice(ProductionResource, amountToSold) * amountToSold) / 100 * Settlement.GetTaxRate();
        Settlement.GetOwner().CollectTaxes(tax);
        ChangeGold(tax, false);
        Debug.Log($"{StratType} payed {tax} to feudal");
    }
}
