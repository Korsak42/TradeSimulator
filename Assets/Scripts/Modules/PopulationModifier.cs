using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class PopulationModifier : MonoBehaviour
{
    public static PopulationModifier instance;

    private void Awake()
    {
        instance = this;
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
        int rateConsumption = CalculateRateConsumption(amountConsumpted, amountNeeded);

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

    public int CalculateRateConsumption(double amountConsumpted, double amountNeeded)
    {
        int returnValue = 0;
        float step = 0.02f;
        if (amountConsumpted > amountNeeded)
            returnValue = (int)(((amountConsumpted / amountNeeded - 1) * 100)/step);
        else if (amountConsumpted > amountNeeded)
            returnValue = (int)(((amountConsumpted / amountNeeded) * 100)/step);
        else
            returnValue = (int)(1/step);

        return returnValue;
    }

    public void Test(Strat strat, double amountConsumpted, double amountNeeded)
    {

    }
}
