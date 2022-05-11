using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementFactory
{
    public static MarketBuilder Market;
    public static VillageBuilder Village;
    public static TownBuilder Town;
    public static CityBuilder City;
    public static CapitalBuilder Capital;

    public static void FactoryInit()
    {
        GameObject go = GameObject.Find("SettlementBuilder");
        Market = go.GetComponent<MarketBuilder>();
        Village = go.GetComponent<VillageBuilder>();
        Town = go.GetComponent<TownBuilder>();
        City = go.GetComponent<CityBuilder>();
        Capital = go.GetComponent<CapitalBuilder>();
    }
    public static void CreateSettlement(EnumCityClasses cityClass, HexCell cell)
    {
        if (Market == null)
            FactoryInit();
        var m = Market.Create();
        switch (cityClass)
        {
            case EnumCityClasses.Village:
                {
                    Village.Create(cell, m);
                    break;
                }
            case EnumCityClasses.Town:
                {
                    Town.Create(cell, m);
                    break;
                }
            case EnumCityClasses.City:
                {
                    City.Create(cell, m);
                    break;
                }
            case EnumCityClasses.Capital:
                {
                    Capital.Create(cell, m);
                    break;
                }
        }
    }
}
