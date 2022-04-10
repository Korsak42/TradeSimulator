using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[ShowOdinSerializedPropertiesInInspector]
public class StratPlanningModule : MonoBehaviour
{
   /* public DataKeeper DataKeeper;

    private Dictionary<Resource, float> needs = new Dictionary<Resource, float>();
    private List<Resource> needs2 = new List<Resource>();

    private double lastTurnGold;
    private double PlannedToProduce;

    public IStrat strat;

    private void Start()
    {
        strat = GetComponent<IStrat>();
        var warehouse = strat.GetWarehouse();
        warehouse.TestInit();
    }

    [Button]
    public void Test()
    {

    }

    public void OnNewTurnInitialization()
    {
        //needs2.Add(new Resource(EnumResource.ResourceName.Food, strat.GetPopulation()));
        /*for (int i = 0; i < warehouse.Count; i++)
        {
            needs.Add(warehouse[i], CheckReserves(warehouse[i], strat.GetPopulation()));
        }
        PlannedToProduce = 0;
    }
    [Button]
    public double ConvertProductionIntoGold(Resource resource, double amount)
    {
        var market = strat.GetMarket();
        return market.CalculatePrice(resource, amount) * amount;
    }

    public void PotentialProfitOnThisTurn(IProducer prodStrat, double producedGoodsAmount)
    {
        var market = strat.GetMarket();
        var price = market.CalculatePrice(prodStrat.GetProductionResource(), producedGoodsAmount);
        var profit = ConvertProductionIntoGold(prodStrat.GetProductionResource(), producedGoodsAmount);
        var costs = CalculateCosts();

        if (profit > costs)
        {
            double reserveProduct = ((profit - costs) / price) / 2;
            double reserveMoney = profit - reserveProduct * price;

            prodStrat.ReserveProductedResource(reserveProduct);
        }
        else 
        {
            double difference = costs - profit;
            double amountOfReservesToEject = difference / price;

            prodStrat.EjectFromReserves(amountOfReservesToEject);
        }

    }

    public double CalculateCosts()
    {
        double returnValue = 0;
        var market = strat.GetMarket();
        foreach (Resource res in needs2)
        {
            returnValue = market.CalculateSellPrice(res, res.Amount);
        }
        return returnValue;
    }























    public List<Resource> CreateShopList()
    {
        List<Resource> shopList = new List<Resource>();
        var warehouse = strat.GetWarehouse();
        
        foreach (KeyValuePair<Resource, float> pair in needs)
        {
            var res = pair.Key;
            if (warehouse.GetAmount(res.Name) < pair.Value * warehouse.GetAmount(res.Name))
            { 
                res.Amount = pair.Value * warehouse.GetAmount(res.Name);
                shopList.Add(res);
            }
        }
        shopList = SortListByPriority(shopList);
        return shopList;
    }

    public void FinalizeShopList()
    {
        List<Resource> shopList = new List<Resource>(CreateShopList());
        var cost = CalculateShopListCost(shopList);

    }

    private double CalculateShopListCost(List<Resource> resources)
    {
        double totalCost = 0;
        foreach(Resource r in resources)
        {
            totalCost += r.Amount * DataKeeper.instance.GetResourceList().GetDefaultPrice(r.Name);
        }
        return totalCost;
    }

    public float CheckReserves(Resource resource, double needsAmount)
    {
        var warehouse = strat.GetWarehouse();
        return (float)(needsAmount / warehouse.GetAmount(resource.Name));
    }

    public double CalculateAvailableGold(double currentTurnCost)
    {
        double returnGold = 0;
        /*  var resourceOnWarehouse = strat.GetWarehouse().GetAmount(strat.GetProductionResource().Name);

          double currentGold = strat.GetGold();
          double currentTurnProduction = strat.CalculateProducedResource();
          float price = DataKeeper.instance.GetDefaultPrice(strat.GetProductionResource().Name);
          double profitThisTurn = currentTurnProduction * price;
          if (profitThisTurn > lastTurnGold)
          { 
              returnGold = lastTurnGold; 
          }
          else if ((profitThisTurn - lastTurnGold) / price < resourceOnWarehouse)
          {
              var needTakeFromWarehouse = (lastTurnGold - profitThisTurn) / price;
              CheckReserves(strat.GetProductionResource(), needTakeFromWarehouse);
              PlannedToProduce = needTakeFromWarehouse;
              returnGold = lastTurnGold;
          }
          else
          {
              PlannedToProduce = resourceOnWarehouse;
              returnGold = (PlannedToProduce + currentTurnProduction) * price;
          }
        
        return returnGold;
    }

    public double CalculateCurrentTurnGold()
    {
        double neededGold = 0;
        foreach (KeyValuePair<Resource, float> pair in needs)
        {
            neededGold += pair.Key.Amount * pair.Value * DataKeeper.instance.GetResourceList().GetDefaultPrice(pair.Key.Name);
        }
        return neededGold;
    }

    public float ChangeConsumptionRateOfResource(double amountOnWarehouse, double amountOfNeeds)
    {
        return (float)(amountOfNeeds / amountOnWarehouse);
    }
    
    public List<Resource> SortListByPriority(List<Resource> resources)
    {
        List<Resource> returnList = resources;
        returnList.Sort(Compare);
        return returnList;
    }

    private int Compare(Resource x, Resource y)
    {
        if (x.Priority > y.Priority)
            return 1;
        else if (x.Priority < y.Priority)
            return -1;
        else
            return 0;
    }
*/
}
