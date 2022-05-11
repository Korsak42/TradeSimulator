using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalBuilder : MonoBehaviour
{
    public GameObject CapitalPrefab;
    public void Create(HexCell cell, GameObject market)
    {
        var settlement = Instantiate(CapitalPrefab);
        settlement.transform.SetParent(cell.transform);
        market.transform.SetParent(settlement.transform);
        var settlementClass = settlement.GetComponent<Capital>();
        settlementClass.GlobalInit();
        var strats = new List<EnumStrats>(settlementClass.StratsInit);
        foreach (EnumStrats strat in strats)
        {
            StratFactory.CreateStrat(settlement.gameObject, strat);
        }
        
    }
}
