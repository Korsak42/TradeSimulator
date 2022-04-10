using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISeller
{
    void SellGood(EnumResource.ResourceName resourceName, double amount);
}
