using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[Serializable]
public class Resource
{
    
    public EnumResource.ResourceName Name;
    public double Amount;
    public float Price;
    public int Priority;

    public Resource()
    {

    }
    public Resource(EnumResource.ResourceName name, double amount)
    {
        Name = name;
        Amount = amount;
    }
    public Resource(EnumResource.ResourceName name, double amount, float price, int priority)
    {
        Name = name;
        Amount = amount;
        Price = price;
        Priority = priority;
    }

    public void Add(double amount)
    {
        Amount += amount;
    }

    public void Subtract(double amount)
    {
        Amount -= amount;
    }

    public void ConsumeBy(IConsumer consumer, double amount)
    {

    }
}
