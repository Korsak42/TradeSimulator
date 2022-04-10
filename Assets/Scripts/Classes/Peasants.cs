using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Linq;

public class Peasants : MonoBehaviour, IStrat, IConsumer, IBuyer, ISeller, IProducer
{
    public ISettlement Settlement;

    public Warehouse Warehouse;
    public StratPlanningModule PlanningModule;
    public Market Market;

    public List<Resource> Needs;

    [SerializeField]
    private float consumeRate;
    [SerializeField]
    private float productivityRate;


    private double gold;
    public double Gold
    {
        set
        {
            gold = value;
            GoldUpdate?.Invoke(gold);
        }
        get => gold;
    }
    private double population;
    public double Population
    {
        set
        {
            population = value;
            PopulationUpdate?.Invoke(population);
        }
        get => population;
    }

    public Resource ProductionResource;
    public double StarvedPopulation;
    public int DaysStarved;

    private float happy;
    public float Happy
    {
        set
        {
            happy = value;
            HappyUpdate?.Invoke(happy);
        }
        get => happy;
    }

    private float health;
    public float Health
    {
        set
        {
            health = value;
            HealthUpdate?.Invoke(health);
        }
        get => health;
    }

    public Action<double> GoldUpdate;
    public Action<double> PopulationUpdate;

    public Action<float> HappyUpdate;
    public Action<float> HealthUpdate;

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
    #region IStrat
    public void GlobalInit()
    {
        ProductionResource = DataKeeper.instance.GetResourceList().GetRandomResource();
        InitNeeds();
        SetProductionResource();
    }
    public void Initialization()
    {
        StarvedPopulation = 0;
    }


    public double GetPopulation()
    {
        return population;
    }

    public double GetGold()
    {
        return gold;
    }

    public void ChangePopulation(float _population)
    {
        Population = _population;
    }

    public void ChangeGold(float _gold)
    {
        Gold = _gold;
    }
    public float GetHeatlh()
    {
        return health;
    }

    public float GetHappy()
    {
        return happy;
    }

    public void ChangeHealth(float amount, bool isPositive)
    {
        if (isPositive)
            Health += amount;
        else
            Health -= amount;
    }

    public void ChangeHappy(float amount, bool isPositive)
    {
        if (isPositive)
            Happy += amount;
        else
            Happy -= amount;
    }



    public void ChangeGold(double amount, bool isPositive)
    {
        if (isPositive)
            Gold += amount;
        else
            Gold -= amount;
    }


    public void ChangePopulation(double amount, bool isPositive)
    {
        if (isPositive)
            Population += amount;
        else
            Population -= amount;
    }
    public Warehouse GetWarehouse()
    {
        return Warehouse;
    }

    public Market GetMarket()
    {
        return Market;
    }

    public float GetConsumeRate()
    {
        return consumeRate;
    }

    public void ChangeConsumeRate(float amount, bool isPositive)
    {
        if (isPositive)
        {
            consumeRate += amount;
        }
        else
        {
            consumeRate -= amount;
        }
    }

    public void ChangeProductivityRate(float amount, bool isPositive)
    {
        if (isPositive)
        {
            productivityRate += amount;
        }
        else
        {
            productivityRate -= amount;
        }
    }
#endregion IStrat
#region IConsumer
    public void ConsumeFood(double amount)
    {
        var consumableResource = EnumResource.ResourceName.Food;
        var amountToConsume = population * consumeRate;
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
        var amountToConsume = Settlement.GetWorkArea() * productivityRate;
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
        if (population > square)
        {
            var producedResourceAmount = square * productivityRate * DataKeeper.instance.GetResourceList().GetDefaultAmount(resource.Name);
            Warehouse.ChangeAmount(ProductionResource.Name, producedResourceAmount, true);
            if (producedResourceAmount > GetResourceFromNeeds(resource.Name).Amount)
                SellGood(ProductionResource.Name, producedResourceAmount - GetResourceFromNeeds(resource.Name).Amount);
        }
        else
        {
            var producedResourceAmount = population * DataKeeper.instance.GetResourceList().GetDefaultAmount(resource.Name);
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
    public void BuyResource(Resource resource, double amount)
    {
        var price = Market.CalculateBuyPrice(resource.Name, amount);
        var cost = price * amount;
        if (gold > 1)
        {
            if (gold > cost)
            {
                MakePurchase(resource.Name, amount);
            }
            else
            {
                var newAmount = gold / price;
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
        var restockAmount = population / DataKeeper.instance.Constants.RestockDivider;
        MakePurchase(resourceName, restockAmount);
    }

    #endregion IByuer
}
