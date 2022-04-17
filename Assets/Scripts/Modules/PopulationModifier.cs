using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class PopulationModifier : MonoBehaviour
{

    public void SetConsumptionConsequences(Strat strat, double amountConsumpted, double amountNeeded)
    {
        if (amountNeeded < amountConsumpted)
        {
            strat.ChangeHappy(DataKeeper.instance.Constants.MinFloatStep * strat.GetConsumeRate(), true);
            strat.ChangeHealth(DataKeeper.instance.Constants.MinFloatStep * strat.GetConsumeRate(), true);
            strat.ChangeConsumeRate(DataKeeper.instance.Constants.MinFloatStep, true);
            strat.ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep, true);
        }
        else
        {
            strat.ChangeHappy(DataKeeper.instance.Constants.MinFloatStep * strat.GetConsumeRate(), false);
            strat.ChangeHealth(DataKeeper.instance.Constants.MinFloatStep * strat.GetConsumeRate(), false);
            strat.ChangeConsumeRate(DataKeeper.instance.Constants.MinFloatStep, false);
            strat.ChangeProductivityRate(DataKeeper.instance.Constants.MinFloatStep, false);
        }
    }

    [Button]
    public void PopulationChange(Strat strat, double amountConsumpted, double amountNeeded)
    {
        if (amountConsumpted >= amountNeeded)
        {
            CalculateBirths(strat, amountConsumpted, amountNeeded);
        }
        else
        {
            CalculateDeaths(strat, amountConsumpted, amountNeeded);
        }
    }

    public void CalculateBirths(Strat strat, double amountConsumpted, double amountNeeded)
    {
        var rateConsumption = (int)((amountConsumpted / amountNeeded - 1) * 100);
        var BirthRate = (DataKeeper.instance.Constants.BirthRateMax - DataKeeper.instance.Constants.BirthRateMin) / 100 * rateConsumption;
        var amountOfBirths = BirthRate * strat.GetPopulation();
        strat.ChangePopulation(amountOfBirths, true);
        Debug.Log("Родилось " + amountOfBirths + " крестьян");
    }
    
    public void CalculateDeaths(Strat strat, double amountConsumpted, double amountNeeded)
    {        
        var rateConsumption = (int)((amountNeeded / amountConsumpted - 1) * 100);
        var DeathRate = (DataKeeper.instance.Constants.BirthRateMax - DataKeeper.instance.Constants.BirthRateMin) / 100 * rateConsumption;
        var amountOfDeaths = DeathRate * strat.GetPopulation();
        strat.ChangePopulation(amountOfDeaths, false);
        Debug.Log("Умерло " + amountOfDeaths + " крестьян");
    }

    public void Test(Strat strat, double amountConsumpted, double amountNeeded)
    {

    }
}
