using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISeller
{
    void SubsribeSeller();
    void SellGood(Resource resource, double amount);
}
