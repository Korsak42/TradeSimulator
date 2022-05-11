using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldiersBuilder : MonoBehaviour
{
    public GameObject SoldiersPrefab;
    public void Create(GameObject settlement)
    {
        var soldiers = Instantiate(SoldiersPrefab, settlement.transform);
        soldiers.GetComponent<Soldiers>().GlobalInit();
    }
}
