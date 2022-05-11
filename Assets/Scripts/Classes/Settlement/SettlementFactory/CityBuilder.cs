using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{
    public GameObject CityPrefab;
    public void Create(HexCell cell, GameObject market)
    {
        var settlement = Instantiate(CityPrefab);
        settlement.transform.SetParent(cell.transform);
        market.transform.SetParent(settlement.transform);
        var settlementClass = settlement.GetComponent<City>();
        settlementClass.GlobalInit();
        var strats = new List<EnumStrats>(settlementClass.StratsInit);
        foreach (EnumStrats strat in strats)
        {
            StratFactory.CreateStrat(settlement.gameObject, strat);
        }
        
    }
}
