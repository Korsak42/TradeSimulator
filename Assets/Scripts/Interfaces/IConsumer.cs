using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumer
{
    double Consume(EnumResource.ResourceName resourceName, double amount);
    void ConsumeFood(double amount);
    
}
