using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeggarsBuilder : MonoBehaviour
{
    public GameObject BeggarsPrefab;

    public void Create(GameObject settlement)
    {
        var beggars = Instantiate(BeggarsPrefab, settlement.transform);
        beggars.GetComponent<Beggars>().GlobalInit();
    }
}
