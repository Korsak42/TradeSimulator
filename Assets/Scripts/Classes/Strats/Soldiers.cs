using System.Collections.Generic;
using UnityEngine;

public class Soldiers : Serviceman
{

    public override void GlobalInit()
    {
        StratType = EnumStrats.Soldiers;
        base.GlobalInit();
        
    }
    public override void ServiceWork()
    {
        if (GetHappy() > 1)
        {
            Guard();
        }
        else
        {
            Robbery(Settlement.GetStrats());
        }
    }

    public void Guard()
    {
        Consume(ResourceFactory.CreateResource(EnumResource.ResourceName.Weapon));
    }

    public void Robbery(List<Strat> strats)
    {
        double stolenMoney = 0;
        foreach (IStrat strat in strats)
        {
            var sMoney = GetPopulation() * Random.Range(0.1f, 3f);
            strat.ChangeGold(sMoney, false);
            stolenMoney += sMoney;
            strat.ChangeHappy(DataKeeper.instance.Constants.MinFloatStep, false);
        }
        ChangeGold(stolenMoney, true);
    }
}
