using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[ShowOdinSerializedPropertiesInInspector]
public class TurnRepeater : MonoBehaviour
{
    [Button]
    public void TurnCycle()
    {
        ProduceCycle(GlobalData.instance.Producers);
        SellCycle(GlobalData.instance.Producers);
        BuyCycle(GlobalData.instance.Buyers);
        ConsumeCycle(GlobalData.instance.Consumers);
    }

    public void ConsumeCycle(List<IConsumer> consumers)
    {
        foreach (Strat consumer in consumers)
        {
            consumer.ConsumeCycle();
        }
    }

    public void ProduceCycle(List<IProducer> producers)
    {
        foreach(IProducer producer in producers)
        {
            producer.Produce();
        }
    }

    public void SellCycle(List<IProducer> producers)
    {
        foreach (IProducer producer in producers)
        {
            producer.SellProductedResource();
        }
    }

    public void BuyCycle(List<IBuyer> buyers)
    {
        foreach(IBuyer buyer in buyers)
        {
            buyer.RestockReserves();
        }
    }

    public void ServiceWorkCycle(List<IServiceman> servicemen)
    {
        foreach(IServiceman serviceman in servicemen)
        {
            serviceman.ServiceWork();
        }
    }
}
