using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtisansBuilder : MonoBehaviour
{
    public GameObject ArtisansPrefab;
    public void Create(GameObject settlement)
    {
        var artisans = Instantiate(ArtisansPrefab, settlement.transform);
        artisans.GetComponent<Artisans>().GlobalInit();
    }
}
