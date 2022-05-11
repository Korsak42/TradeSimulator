using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : Settlement
{
    public override void GlobalInit()
    {
        SettlementClass = EnumCityClasses.City;
        base.GlobalInit();
    }
}
