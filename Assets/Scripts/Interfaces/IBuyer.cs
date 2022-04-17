using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuyer
{
    void SubsribeBuyer();
    void BuyResource(Resource resource, double amount);
    void MakePurchase(Resource resource, double amount);
}
