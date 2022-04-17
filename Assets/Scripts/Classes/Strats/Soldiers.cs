using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiers : Serviceman
{
    public override void ServiceWork(double amountConsumpted, double amountNeeded)
    {
        if (GetHappy() > 1)
        {
            if (amountConsumpted > amountNeeded)
            {
                
            }
            else
            {
                Settlement.ChangeCrimeRate(DataKeeper.instance.Constants.MinFloatStep, false);
            }
        }
        else
        {

        }
    }

    public void Guard()
    {
        Consume(ResourceFactory.CreateResource(EnumResource.ResourceName.Weapon));
    }

    public void Robbery(List<IStrat> strats)
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
