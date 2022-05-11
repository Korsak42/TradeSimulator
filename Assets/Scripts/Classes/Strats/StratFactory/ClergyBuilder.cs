using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClergyBuilder : MonoBehaviour
{
    public GameObject ClergyPrefab;
    public void Create(GameObject settlement)
    {
        var clergy = Instantiate(ClergyPrefab, settlement.transform);
        clergy.GetComponent<Clergy>().GlobalInit();
    }
}
