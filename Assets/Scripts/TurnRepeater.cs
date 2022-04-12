using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRepeater : MonoBehaviour
{
    public ISettlement Settlement;
    public List<IStrat> Strats;
    public List<IProducer> Producers;
    public List<IServiceman> Servicemen;
    public List<IBuyer> Buyers;
    public List<IConsumer> Consumers;
    public List<ISeller> Sellers;
    public INoble Noble;

    public void SubsribeStrat(IStrat strat)
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

    public void Initialized()
    {

    }

    public void TurnCycle()
    {

    }
}
