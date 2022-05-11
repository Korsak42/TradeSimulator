using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownBuilder : MonoBehaviour
{
    public GameObject TownPrefab;
    public void Create(HexCell cell, GameObject market)
    {
        var settlement = Instantiate(TownPrefab);
        settlement.transform.SetParent(cell.transform);
        market.transform.SetParent(settlement.transform);
        var settlementClass = settlement.GetComponent<Town>();
        settlementClass.GlobalInit();
        var strats = new List<EnumStrats>(settlementClass.StratsInit);
        foreach (EnumStrats strat in strats)
        {
            StratFactory.CreateStrat(settlement.gameObject, strat);
        }
        
    }
}
