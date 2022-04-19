
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[ShowOdinSerializedPropertiesInInspector]
public class Strat : MonoBehaviour, IStrat, IConsumer, IBuyer, ISeller
{
    public EnumStrats StratType;

    public ISettlement Settlement;

    public TurnRepeater TurnRepeater;

    public Warehouse Warehouse;
    public StratPlanningModule PlanningModule;
    public DemandModule DemandModule;
    public Market Market;

    public List<Resource> Needs;

    
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
    private void Start()
    {
        GlobalLinkStrat();   
    }
    public void GlobalLinkStrat()
    {
        GlobalLink.instance.TurnRepeaterLink(this);
        Settlement = GetComponentInParent<ISettlement>();
        Warehouse = GetComponent<Warehouse>();
        DemandModule = GetComponent<DemandModule>();
        SubsribeStrat();
        SubsribeStratToInitialization();
    }

    private void SubsribeStratToInitialization()
    {
        InitializationModule.instance.SubscribeStrat(this);
    }

    public void GlobalInit()
    {
        StratType = EnumStrats.Peasants;
        GlobalLinkStrat();
        Settlement = GetComponent<ISettlement>();
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
        TurnRepeater.SubsribeStrat(this);
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
        TurnRepeater.SubsribeConsumer(this);
    }

    public void ConsumeCycle()
    {
        foreach (KeyValuePair<Resource, double> pair in DemandModule.demands)
        {
            Consume(pair.Key);
        }
    }
    public void Consume(Resource resource)
    {
        var amountToConsume = resource.GetAmountToConsume(this);
        var availiableResourceOnWarehouse = Warehouse.GetAmount(resource);
        var amountConsumed = Math.Min(amountToConsume, availiableResourceOnWarehouse);
        Warehouse.ChangeAmount(resource, -amountConsumed);
        resource.ConsumeBy(this, amountToConsume, amountConsumed);
        Debug.Log(StratType + "consumed " + amountConsumed + " of " + resource.Name);
    }

    public void SubsribeSeller()
    {
        TurnRepeater.SubsribeSeller(this);
    }
    [Button]
    public void SellGood(Resource resource, double amount)
    {
        var profit = Market.CalculateSellPrice(resource, amount) * amount;
        Warehouse.ChangeAmount(resource, amount);
        Market.Warehouse.ChangeAmount(resource, amount);
        Gold += profit;
        Debug.Log(amount + " " + resource.Name + "was sold by " + profit);
    }

    public void SubsribeBuyer()
    {
        TurnRepeater.SubsribeBuyer(this);
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
            Market.Warehouse.ChangeAmount(resource, amount);
            Warehouse.ChangeAmount(resource, amount);
            ChangeGold(Market.CalculateBuyPrice(resource, amount) * amount, false);
        }
        else
        {
            var newAmount = Market.Warehouse.GetAmount(resource);
            Market.Warehouse.ChangeAmount(resource, newAmount);
            Warehouse.ChangeAmount(resource, newAmount);
            ChangeGold(Market.CalculateBuyPrice(resource, newAmount) * newAmount, false);
        }
    }

    public void Restock(Resource resource)
    {
        var restockAmount = Population / DataKeeper.instance.Constants.RestockDivider;
        MakePurchase(resource, restockAmount);
    }
}
