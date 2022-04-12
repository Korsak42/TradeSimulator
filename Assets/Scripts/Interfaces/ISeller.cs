using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISeller
{
    void SubsribeSeller();
    void SellGood(EnumResource.ResourceName resourceName, double amount);
}
