using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capital : Settlement
{
    public override void GlobalInit()
    {
        SettlementClass = EnumCityClasses.Capital;
        base.GlobalInit();
    }
}
