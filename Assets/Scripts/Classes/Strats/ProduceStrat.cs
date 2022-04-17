using Sirenix.OdinInspector;
public class ProduceStrat : Strat, IProducer
{
    public Resource ProductionResource;
    public void ProducerGlobalInit()
    {
        SetProductionResource();
        SubsribeProducer();
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
        ProductionResource = ResourceFactory.CreateResource(EnumResource.ResourceName.Tools);
    }

    public void SubsribeProducer()
    {
        TurnRepeater.SubsribeProducer(this);
    }
}
