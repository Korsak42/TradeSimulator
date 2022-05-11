using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VillageBuilder : MonoBehaviour
{
    public GameObject VillagePrefab;
    public void Create(HexCell cell, GameObject market)
    {
        var settlement = Instantiate(VillagePrefab);
        settlement.transform.SetParent(cell.transform);
        market.transform.SetParent(settlement.transform);
        var settlementClass = settlement.GetComponent<Village>();
        settlementClass.GlobalInit();
        var strats = new List<EnumStrats>(settlementClass.StratsInit);
        foreach (EnumStrats strat in strats)
        {
            StratFactory.CreateStrat(settlement.gameObject, strat);
        }
        
    }

}
