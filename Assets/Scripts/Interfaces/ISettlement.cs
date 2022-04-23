using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISettlement 
{
    void GlobalInit();
    void ChangeCrimeRate(float amount, bool isPositive);
    void ChangeCrimeRate(float _crimeRate);
    void ChangeHappy(float _crimeRate);
    void ChangeHealth(float _piety);
    void ChangeInfrastructure(float _infrastructure);
    void ChangeInfrastructure(float amount, bool isPositive);
    void ChangePiety(float amount, bool isPositive);
    void ChangePiety(float _piety);
    void CreateAdminArea(int amount);
    void CreateDefenseArea(int amount);
    void CreateLivingArea(int amount);
    void CreateWorkerArea(int amount);
    List<Strat> GetStrats();
    float GetCrimeRate();
    float GetInfrastucture();
    float GetPiety();
    int GetWorkArea();
    int GetFreeArea();
    int GetDefenseArea();
    void SubscribeStrat(Strat strat);

    Market GetMarket();

    Noble GetOwner();
    void SetOwner(Noble noble);
    float GetTaxRate();
}
