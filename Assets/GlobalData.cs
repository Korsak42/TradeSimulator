using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[ShowOdinSerializedPropertiesInInspector]
public class GlobalData : MonoBehaviour
{
    public static GlobalData instance;

    public List<ISettlement> Settlements = new List<ISettlement>();
    public List<Strat> Strats = new List<Strat>();
    public List<IProducer> Producers = new List<IProducer>();
    public List<IServiceman> Servicemen = new List<IServiceman>();
    public List<IBuyer> Buyers = new List<IBuyer>();
    public List<IConsumer> Consumers = new List<IConsumer>();
    public List<ISeller> Sellers = new List<ISeller>();
    public List<INoble> Nobles = new List<INoble>();
    public List<IMarket> Markets = new List<IMarket>();

    private void Awake()
    {
        instance = this;
    }

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
        Nobles.Add(strat);
    }
    public void SubscribeSettlement(ISettlement settlemen)
    {
        Settlements.Add(settlemen);
    }

    public void SubscribeMarket(IMarket market)
    {
        Markets.Add(market);
    }
}
