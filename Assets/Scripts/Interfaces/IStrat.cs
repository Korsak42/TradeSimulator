using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStrat
{
    void SubsribeStrat();
    EnumStrats GetStratType();
    double GetPopulation();
    double GetGold();
    void ChangePopulation(float _population);
    void ChangeGold(float _gold);
    float GetHeatlh();
    float GetHappy();
    void GlobalInit();
    void ChangeGold(double amount, bool isPositive);
    void ChangePopulation(double amount, bool isPositive);
    Warehouse GetWarehouse();
    Market GetMarket();
    void ChangeHealth(float amount, bool isPositive);
    void ChangeHappy(float amount, bool isPositive);
}
