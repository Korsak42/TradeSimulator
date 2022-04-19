using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[ShowOdinSerializedPropertiesInInspector]
public class TurnRepeater : MonoBehaviour
{
    public ISettlement Settlement;
    public List<Strat> Strats = new List<Strat>();
    public List<IProducer> Producers = new List<IProducer>();
    public List<IServiceman> Servicemen = new List<IServiceman>();
    public List<IBuyer> Buyers = new List<IBuyer>();
    public List<IConsumer> Consumers = new List<IConsumer>();
    public List<ISeller> Sellers = new List<ISeller>();
    public INoble Noble;

    public void SubsribeStrat(Strat strat)
    {
        Strats.Add(strat);
    }
    public void SubsribeProducer(IProducer strat)
    {
        Producers.Add(strat);
    }
    public void SubsribeServiceman(IServiceman strat)
    {
        Servicemen.Add(strat);
    }
    public void SubsribeBuyer(IBuyer strat)
    {
        Buyers.Add(strat);
    }
    public void SubsribeConsumer(IConsumer strat)
    {
        Consumers.Add(strat);
    }
    public void SubsribeSeller(ISeller strat)
    {
        Sellers.Add(strat);
    }

    public void SubscribeNoble(INoble strat)
    {
        Noble = strat;
    }
    [Button]
    public void TurnCycle()
    {
        ConsumeCycle();
    }

    public void ConsumeCycle()
    {
        foreach (Strat strat in Strats)
        {
            if (strat.GetPopulation() > 1)
            {
                strat.ConsumeCycle();
            }
        }
    }
}
