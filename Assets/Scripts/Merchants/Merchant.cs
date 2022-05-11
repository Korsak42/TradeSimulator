using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Merchant : MonoBehaviour
{
    private HexCell currentPosition;
    private HexCell destinationPoint;

    public double Money;
    public int TotalCapacity;

    public Warehouse Warehouse;

    public void SubscribeMerchant()
    {
        throw new NotImplementedException();
    }

    public void NextTurn()
    {
        if(currentPosition.isCity)
        {
            DecideWhatToBuy();
            CreatePathToAnotherSettlement();
        }
        else
        {
            Move();
        }
    }

    public void Move()
    {

    }

    public void CreatePathToAnotherSettlement()
    {

    }

    public void DecideWhatToBuy()
    {

    }

    public void FindBestGood()
    {
        
    }
}
