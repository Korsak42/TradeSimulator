using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketBuilder : MonoBehaviour
{
    public GameObject MarketPrefab;
    public GameObject Create()
    {
        return Instantiate(MarketPrefab);
    }
}
