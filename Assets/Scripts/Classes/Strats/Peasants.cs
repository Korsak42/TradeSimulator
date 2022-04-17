using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Linq;

public class Peasants : ProduceStrat, IBuyer, ISeller
{
    [Button]
    public Resource GetResourceFromNeeds(EnumResource.ResourceName resourceName)
    {
        return Needs.FirstOrDefault(x => x.Name == resourceName);
    }
    public override void Produce()
    {
        base.Produce();
        var producedFoodAmount = UnityEngine.Random.Range(1, Settlement.GetFreeArea()) * ProductivityRate;
        Warehouse.ChangeAmount(ResourceFactory.CreateResource(EnumResource.ResourceName.Food), producedFoodAmount);
    }
}
