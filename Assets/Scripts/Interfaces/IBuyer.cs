using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuyer
{
    void BuyResource(Resource resource, double amount);
    void MakePurchase(EnumResource.ResourceName resourceName, double amount);
}
