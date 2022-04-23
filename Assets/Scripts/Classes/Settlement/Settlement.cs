using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Sirenix.OdinInspector;
[ShowOdinSerializedPropertiesInInspector]

public class Settlement : MonoBehaviour, ISettlement
{
    public string Name;

    public List<Strat> Strats;
    public Noble Owner;
    public float TaxRate;

    public Market SettlementMarket;

    public EnumCityClasses cityClass;
    public int CityLandmass;
    private float piety;
    public float Piety
    {
        set
        {
            piety = value;
            PietyUpdate?.Invoke(piety);
        }
        get => piety;
    }
    private float crimeRate;
    public float CrimeRate
    {
        set
        {
            crimeRate = value;
            CrimeRateUpdate?.Invoke(crimeRate);
        }
        get => crimeRate;
    }

    private float infrastructure;
    public float Infrastructure
    {
        set
        {
            infrastructure = value;
            InfrastructureUpdate?.Invoke(infrastructure);
        }
        get => infrastructure;
    }


    public Dictionary<EnumCityStructure, int> CityParts = new Dictionary<EnumCityStructure, int>();

    public Action<float> InfrastructureUpdate;
    public Action<float> PietyUpdate;
    public Action<float> CrimeRateUpdate;

    private void Awake()
    {
        SubscribeSettlement();
        SettlementMarket = GetComponentInChildren<Market>();
    }

    public void GlobalInit()
    {
        SetCityStructure();
    }

    public float GetTaxRate()
    {
        return TaxRate;
    }
    public Noble GetOwner()
    {
        return Owner;
    }

    public void SetOwner(Noble noble)
    {
        Owner = noble;
    }

    public float GetInfrastucture()
    {
        return infrastructure;
    }
    public float GetPiety()
    {
        return piety;
    }

    public float GetCrimeRate()
    {
        return crimeRate;
    }

    public void ChangeInfrastructure(float _infrastructure)
    {
        Infrastructure = _infrastructure;
    }

    public void ChangeInfrastructure(float amount, bool isPositive)
    {
        if (isPositive)
            Infrastructure += amount;
        else
            Infrastructure -= amount;
    }

    public void ChangePiety(float _piety)
    {
        Piety = _piety;
    }

    public void ChangePiety(float amount, bool isPositive)
    {
        if (isPositive)
            Piety += amount;
        else
            Piety -= amount;
    }

    public void ChangeCrimeRate(float _crimeRate)
    {
        CrimeRate = _crimeRate;
    }
    public void ChangeCrimeRate(float amount, bool isPositive)
    {
        if (isPositive)
            CrimeRate += amount;
        else
            CrimeRate -= amount;
    }
    public List<Strat> GetStrats()
    {
        return Strats;
    }

    public void SubscribeStrat(Strat strat)
    {
        Strats.Add(strat);
    }

    public void SubscribeSettlement()
    {
        GlobalData.instance.SubscribeSettlement(this);
    }

    public int GetFreeArea()
    {
        return CityParts[EnumCityStructure.FreeLandmass];
    }

    public int GetDefenseArea()
    {
        return CityParts[EnumCityStructure.DefenseArea];
    }
    public int GetWorkArea()
    {
        return CityParts[EnumCityStructure.WorkArea];
    }
    [Button]
    public void SetCityStructure()
    {
        CityParts = DataKeeper.instance.GetCityStructure(cityClass);
        foreach (var key in CityParts.Keys.ToList())
        {
            CityParts[key] = (int)(CityLandmass / 100 * CityParts[key]);
        }
    }

    public void CreateLivingArea(int amount)
    {
        AddArea(EnumCityStructure.LivingArea, amount);
        SubstractFreeLandmass(amount);
    }

    public void CreateWorkerArea(int amount)
    {
        AddArea(EnumCityStructure.WorkArea, amount);
        SubstractFreeLandmass(amount);
    }

    public void CreateDefenseArea(int amount)
    {
        AddArea(EnumCityStructure.DefenseArea, amount);
        SubstractFreeLandmass(amount);
    }

    public void CreateAdminArea(int amount)
    {
        AddArea(EnumCityStructure.AdminArea, amount);
        SubstractFreeLandmass(amount);
    }

    private void AddArea(EnumCityStructure structure, int amount)
    {
        CityParts[structure] += amount;
    }

    private void SubstractFreeLandmass(int amount)
    {
        CityParts[EnumCityStructure.FreeLandmass] -= amount;
    }

    public void ChangeHappy(float _crimeRate)
    {
        throw new NotImplementedException();
    }

    public void ChangeHealth(float _piety)
    {
        throw new NotImplementedException();
    }

    public Market GetMarket()
    {
        return SettlementMarket;
    }
}
