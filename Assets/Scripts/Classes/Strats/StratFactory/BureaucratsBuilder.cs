using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BureaucratsBuilder : MonoBehaviour
{
    public GameObject BureaucratsPrefab;
    public void Create(GameObject settlement)
    {
        var bureaucrats = Instantiate(BureaucratsPrefab, settlement.transform);
        bureaucrats.GetComponent<Bureaucrats>().GlobalInit();
    }
}
