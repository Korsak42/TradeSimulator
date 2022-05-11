using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : Settlement
{
    public override void GlobalInit()
    {
        SettlementClass = EnumCityClasses.Town;
        base.GlobalInit();
    }
}
