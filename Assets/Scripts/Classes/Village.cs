using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
[ShowOdinSerializedPropertiesInInspector]
public class Village : MonoBehaviour, ISettlement
{
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

    public Action<float> PietyUpdate;
    public Action<float> CrimeRateUpdate;
    public Action<float> InfrastructureUpdate;

    public void GlobalInit()
    {
        SetCityStructure();
    }

    public float GetPiety()
    {
        return piety;
    }

    public float GetCrimeRate()
    {
        return crimeRate;
    }

    public float GetInfrastucture()
    {
        return infrastructure;
    }

    public void ChangeHealth(float _piety)
    {
        Piety = _piety;
    }

    public void ChangeHappy(float _crimeRate)
    {
        CrimeRate = _crimeRate;
    }
    public void ChangeInfrastructure(float _infrastructure)
    {
        Infrastructure = _infrastructure;
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

}
