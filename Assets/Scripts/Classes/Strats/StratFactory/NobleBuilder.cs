using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NobleBuilder : MonoBehaviour
{
    public GameObject NoblePrefab;
    public void Create(GameObject settlement)
    {
        var noble = Instantiate(NoblePrefab, settlement.transform);
        noble.GetComponent<Noble>().GlobalInit();
    }
}
