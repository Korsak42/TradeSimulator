using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProducer
{
    void SubsribeProducer();
    double CalculateProducedResource();
    Resource GetProductionResource();
    void Produce();
    void SetProductionResource();
    void ProducerGlobalInit();
    [Button]
    void SellProductedResource();
}
