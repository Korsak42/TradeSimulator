using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Linq;

public class Peasants : Strat, IConsumer, IBuyer, ISeller, IProducer
{

    private void Awake()
    {
        SubsribeSeller();
        SubsribeProducer();
        SubsribeConsumer();
        SubsribeBuyer();
    }

    public void InitNeeds()
    {
        foreach (Resource r in Needs)
        {
            Needs = DataKeeper.instance.GetListOfResources();
            if (r.Name == EnumResource.ResourceName.Tools)
                r.Amount = Settlement.GetWorkArea();
            else
                r.Amount = Population;
        }
    }
    [Button]
    public Resource GetResourceFromNeeds(EnumResource.ResourceName resourceName)
    {
        return Needs.FirstOrDefault(x => x.Name == resourceName);
    }

    #region IConsumer
    public void SubsribeConsumer()
    {
        TurnRepeater.SubsribeConsumer(this);
    }

    public void ConsumeFood(double amount)
    {
        var consumableResource = EnumResource.ResourceName.Food;
        var amountToConsume = Population * ConsumeRate;
        var consumedAmount = Consume(consumableResource, amountToConsume);
        if (consumedAmount != double.NaN)
        {
            ChangeHealth(DataKeeper.instance.Constants.MinFloatStep, false);
            ChangeHappy(DataKeeper.instance.Constants.MinFloatStep, false);
            ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep, false);
        }
        else
        {
            ChangeHealth(DataKeeper.instance.Constants.MinFloatStep, true);
            ChangeHappy(DataKeeper.instance.Constants.MinFloatStep, true);
            ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep, true);
        }
    }

    public double ConsumeTools(double amount)
    {
        var consumableResource = EnumResource.ResourceName.Tools;
        var amountToConsume = Settlement.GetWorkArea() * ProductivityRate;
        var consumedAmount = Consume(consumableResource, amountToConsume);
        if (consumedAmount != double.NaN)
        {
            ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep, false);
        }
        else
        {
            ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep, true);
        }
        return consumedAmount;
    }

    public double Consume(EnumResource.ResourceName resourceName, double amount)
    {
        if (Warehouse.CheckResourceOnWarehouseAmountMoreThan(resourceName, amount))
        {
            Warehouse.ChangeAmount(resourceName, amount, false);
            return double.NaN;
        }
        else
        {
            var consumedAmount = Warehouse.GetAmount(resourceName);
            Warehouse.ChangeAmount(resourceName, consumedAmount, false);
            return consumedAmount;
        }
    }

    #endregion IConsumer
    #region IProducer
    public void SubsribeProducer()
    {
        TurnRepeater.SubsribeProducer(this);
    }
    public Resource GetProductionResource()
    {
        return ProductionResource;
    }

    public void SetProductionResource()
    {

    }
    public void Produce()
    {
        var resource = GetProductionResource();
        var square = Settlement.GetWorkArea();
        var consumedTools = ConsumeTools(square);
        if (Population > square)
        {
            var producedResourceAmount = square * ProductivityRate * DataKeeper.instance.GetResourceList().GetDefaultAmount(resource.Name);
            Warehouse.ChangeAmount(ProductionResource.Name, producedResourceAmount, true);
            if (producedResourceAmount > GetResourceFromNeeds(resource.Name).Amount)
                SellGood(ProductionResource.Name, producedResourceAmount - GetResourceFromNeeds(resource.Name).Amount);
        }
        else
        {
            var producedResourceAmount = Population * DataKeeper.instance.GetResourceList().GetDefaultAmount(resource.Name);
            Warehouse.ChangeAmount(ProductionResource.Name, producedResourceAmount, true);
            if (producedResourceAmount > GetResourceFromNeeds(resource.Name).Amount)
                SellGood(ProductionResource.Name, producedResourceAmount - GetResourceFromNeeds(resource.Name).Amount);
        }

        
    }

    public double CalculateProducedResource()
    {
        var toolsAmount = Warehouse.GetAmount(EnumResource.ResourceName.Tools);
        if (Population > toolsAmount)
            return toolsAmount;
        else
            return Population;
    }

    #endregion IProducer
    #region ISeller
    public void SubsribeSeller()
    {
        TurnRepeater.SubsribeSeller(this);
    }
    [Button]
    public void SellGood(EnumResource.ResourceName resourceName, double amount)
    {
        var profit = Market.CalculateSellPrice(resourceName, amount) * amount;
        Warehouse.ChangeAmount(ProductionResource.Name, amount, false);
        Market.Warehouse.ChangeAmount(ProductionResource.Name, amount, true);
        Gold += profit;
        Debug.Log(amount + " " + resourceName + "was sold by " + profit);
    }
    #endregion ISeller
    #region IByuer
    public void SubsribeBuyer()
    {
        TurnRepeater.SubsribeBuyer(this);
    }
    public void BuyResource(Resource resource, double amount)
    {
        var price = Market.CalculateBuyPrice(resource.Name, amount);
        var cost = price * amount;
        if (Gold > 1)
        {
            if (Gold > cost)
            {
                MakePurchase(resource.Name, amount);
            }
            else
            {
                var newAmount = Gold / price;
                MakePurchase(resource.Name, newAmount);
            }
        }
        else
            Debug.Log("Not enough gold for purchasing" + resource.Name);
    }

    public void MakePurchase(EnumResource.ResourceName resourceName, double amount)
    {
        if (Market.Warehouse.CheckResourceOnWarehouseAmountMoreThan(resourceName, amount))
        {
            Market.Warehouse.ChangeAmount(resourceName, amount, false);
            Warehouse.ChangeAmount(resourceName, amount, true);
            ChangeGold(Market.CalculateBuyPrice(resourceName, amount) * amount, false);
        }
        else
        {
            var newAmount = Market.Warehouse.GetAmount(resourceName);
            Market.Warehouse.ChangeAmount(resourceName, newAmount, false);
            Warehouse.ChangeAmount(resourceName, newAmount, true);
            ChangeGold(Market.CalculateBuyPrice(resourceName, newAmount) * newAmount, false);
        }
    }

    public void Restock(EnumResource.ResourceName resourceName)
    {
        var restockAmount = Population / DataKeeper.instance.Constants.RestockDivider;
        MakePurchase(resourceName, restockAmount);
    }

    #endregion IByuer
}
