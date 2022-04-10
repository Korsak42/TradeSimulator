using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProducer
{
    double CalculateProducedResource();
    Resource GetProductionResource();
    void Produce();
    void SetProductionResource();
}
