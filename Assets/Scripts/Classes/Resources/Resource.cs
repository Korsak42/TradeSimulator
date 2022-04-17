using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public abstract class Resource
{
    public EnumResource.ResourceName Name;
    public float BasePrice;
    public float BaseAmount;

    public Resource()
    {
        
    }

    public Resource(EnumResource.ResourceName name, float amount, float price)
    {
        Name = name;
        BaseAmount = amount;
        BasePrice = price;
    }

    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }

    public EnumResource.ResourceName GetName()
    {
        return Name;
    }

    public abstract void ConsumeBy(Strat consumer, double amountNeeded, double amountConsumed);

    public abstract double GetAmountToConsume(Strat consumer);
}
