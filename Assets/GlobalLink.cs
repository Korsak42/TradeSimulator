using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLink : MonoBehaviour
{
    public static GlobalLink instance;
    public TurnRepeater TurnRepeater;


    private void Awake()
    {
        instance = this;
    }
    public void TurnRepeaterLink (Strat strat)
    {     
        strat.TurnRepeater = TurnRepeater;
    }
}
