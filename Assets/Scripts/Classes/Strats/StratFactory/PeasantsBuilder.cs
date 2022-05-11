using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantsBuilder : MonoBehaviour
{
    public GameObject PeasantsPrefab;
    public void Create(GameObject settlement)
    {
        var peasants = Instantiate(PeasantsPrefab, settlement.transform);
        peasants.GetComponent<Peasants>().GlobalInit();
    }
}
