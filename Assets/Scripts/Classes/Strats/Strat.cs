
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[ShowOdinSerializedPropertiesInInspector]
public class Strat : MonoBehaviour, IStrat, IConsumer, IBuyer, ISeller
{
    public EnumStrats StratType;

    public ISettlement Settlement;

    public Warehouse Warehouse;

    public DemandModule DemandModule;
    public Market Market;
    
    public float ConsumeRate;
    
    public float ProductivityRate;


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
    private double population = 350;
    public double Population
    {
        set
        {
            population = value;
            PopulationUpdate?.Invoke(population);
        }
        get => population;
    }

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

    [Button]
    public void GlobalLinkStrat()
    {
        Settlement = GetComponentInParent<ISettlement>();
        Settlement.SubscribeStrat(this);
        Warehouse = GetComponent<Warehouse>();
        DemandModule = GetComponent<DemandModule>();
        
        SubsribeStrat();
        SubsribeBuyer();
        SubsribeConsumer();
        SubsribeSeller();
        SubsribeStratToInitialization();
    }

    private void SubsribeStratToInitialization()
    {
        //InitializationModule.instance.SubscribeStrat(this);
    }
    [Button]
    public virtual void GlobalInit()
    {
        GlobalLinkStrat();
        Market = Settlement.GetMarket();
        ChangeHealth(1f, true);
        ChangeHappy(1f, true);
        ChangeProductivityRate(1f, true);
        ChangeConsumeRate(1f, true);
        ChangeGold(UnityEngine.Random.Range(100, 1000));
        Population = 350;
        Warehouse.TestInit();
        DemandModule.GlobalInit(this);
    }

    public void SubsribeStrat()
    {
        GlobalData.instance.SubsribeStrat(this);
    }
    public EnumStrats GetStratType()
    {
        return StratType;
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
        return ConsumeRate;
    }

    public void ChangeConsumeRate(float amount, bool isPositive)
    {
        if (isPositive)
        {
            ConsumeRate += amount;
        }
        else
        {
            ConsumeRate -= amount;
        }
    }

    public void ChangeProductivityRate(float amount, bool isPositive)
    {
        if (isPositive)
        {
            ProductivityRate += amount;
        }
        else
        {
            ProductivityRate -= amount;
        }
    }

    public void SubsribeConsumer()
    {
        GlobalData.instance.SubsribeConsumer(this);
    }

    [Button]
    public void ConsumeCycle()
    {
        List<Resource> resources = new List<Resource>(DemandModule.demands.Keys);
        foreach (Resource resource in resources)
        {
            Consume(resource);
        }
    }
    public void Consume(Resource resource)
    {
        var amountToConsume = resource.GetAmountToConsume(this);
        var availiableResourceOnWarehouse = Warehouse.GetAmount(resource);
        var amountConsumed = Math.Min(amountToConsume, availiableResourceOnWarehouse);
        Warehouse.ChangeAmount(resource, -amountConsumed);
        resource.ConsumeBy(this, amountToConsume, amountConsumed);
        DemandModule.demands[resource] = DemandModule.demands[resource] - amountToConsume;
        PopulationModifier.instance.PopulationChange(this, amountToConsume, amountConsumed);
        Debug.Log(StratType + " consumed " + amountConsumed + " of " + resource.Name);
    }

    public void SubsribeSeller()
    {
        GlobalData.instance.SubsribeSeller(this);
    }

    public void SellGood(Resource resource, double amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException($"You are trying to sell negative amount of {resource.Name} ");
        }
        var profit = Market.CalculateSellPrice(resource, amount) * amount;
        Warehouse.ChangeAmount(resource, -amount);
        Market.Warehouse.ChangeAmount(resource, amount);
        Gold += profit;
        Debug.Log(amount + " " + resource.Name + "was sold by " + profit);
    }

    public void SubsribeBuyer()
    {
        GlobalData.instance.SubsribeBuyer(this);
    }
    public void BuyResource(Resource resource, double amount)
    {
        var price = Market.CalculateBuyPrice(resource, amount);
        var cost = price * amount;
        if (Gold > 1)
        {
            if (Gold > cost)
            {
                MakePurchase(resource, amount);
            }
            else
            {
                var newAmount = Gold / price;
                MakePurchase(resource, newAmount);
            }
        }
        else
            Debug.Log("Not enough gold for purchasing" + resource.Name);
    }

    public void MakePurchase(Resource resource, double amount)
    {
        if (Market.Warehouse.CheckResourceOnWarehouseAmountMoreThan(resource, amount))
        {
            var totalCost = Market.CalculateBuyPrice(resource, amount) * amount;
            Market.Warehouse.ChangeAmount(resource, -amount);
            Warehouse.ChangeAmount(resource, amount);
            ChangeGold(totalCost, false);
            Debug.Log(resource.Name + " was bought in amount of " + amount + ". Total cost:" + totalCost);
        }
        else
        {
            var newAmount = Market.Warehouse.GetAmount(resource);
            var totalCost = Market.CalculateBuyPrice(resource, newAmount) * amount;
            Market.Warehouse.ChangeAmount(resource, -newAmount);
            Warehouse.ChangeAmount(resource, newAmount);
            ChangeGold(totalCost, false);
            Debug.Log(resource.Name + " was bought in amount of " + newAmount + ". Total cost:" + totalCost);
        }
    }
    [Button]
    public void RestockReserves()
    {
        foreach (KeyValuePair<Resource, double> pair in DemandModule.demands)
        {
            Restock(pair.Key, pair.Value);
        }
    }

    public void Restock(Resource resource, double amount)
    {
        var restockAmount = amount;
        MakePurchase(resource, restockAmount);
    }
}
